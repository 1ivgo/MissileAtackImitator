"""Imitation calculation module.

Usage:
python3 trajectorygenerator.py requestFileName responseFileName
"""

import sys
import os
import json
from datetime import datetime
from bezier import Curve
import numpy as np
from numpy import linalg

def calculateImitation(requestFileName, responseFileName):
    requestFile = open(requestFileName)
    request = json.load(requestFile)
    
    stepsCount = request['StepsCount']

    curvesBasisPoints = np.hstack(tuple(map(requestPointToNPPoint, request['AircraftPoints'])))

    at = calculateAircraftTrajectory(curvesBasisPoints, stepsCount)
    response = {'AircraftTrajectory' : list(map(npPointToResponsePoint, 
                                                np.hsplit(at, np.shape(at)[1])))}
    
    missile = request['Missile']
    launchPoint = requestPointToNPPoint(missile['LaunchPoint'])
    direction = requestPointToNPPoint(missile['Direction']) - launchPoint
    velocity = unitVector(direction) * missile['VelocityModule']
    missilePoints = np.hstack((launchPoint, launchPoint + velocity))

    mt = calcProportionalMissile(at, missilePoints, 20, request['StepsCount'])
    
    response['MissileTrajectory'] = list(map(npPointToResponsePoint, 
                                                np.hsplit(mt, np.shape(mt)[1])))

    json.dump(response, open(responseFileName, 'w'))
    return 

def calculateAircraftTrajectory(curvesBasisPoints, stepsCount):
    curves = npPointsToCurves(curvesBasisPoints, 3)

    evaluate = lambda curve: \
        curve.evaluate_multi(np.linspace(0.0, 1.0, stepsCount // len(curves)))

    t = np.hstack(tuple(map(evaluate, curves)))
    return t

def requestPointToNPPoint(p):
    return np.array([[p['x']],[p['y']]], np.float64)

def npPointToResponsePoint(p):
    return {'x': p[0, 0], 'y': p[1, 0]}

def npPointsToCurves(curvesBasisPoints, maxPerCurvePointsCount):
    if curvesBasisPoints.size == 0: 
        return []
    
    result = [Curve.from_nodes(curvesBasisPoints[:, :maxPerCurvePointsCount])]
    result.extend(npPointsToCurves(curvesBasisPoints[:, maxPerCurvePointsCount-1:], 
                                   maxPerCurvePointsCount))
    
    return result

def angle(v):
    return np.arctan2(v[1], v[0])

def unitVector(v):
    return v / linalg.norm(v)

def orthogonalVector(v):
    return np.array([-v[1], v[0]])

def rotate(v, angle):
    cos = np.cos(angle)
    sin = np.sin(angle)
    rotationMatrix = np.array([[cos, -sin],
                               [sin, cos]])
    return np.matmul(rotationMatrix, v)                           

def calcProportionalMissile(aircraftPoints, missilePoints, k, stepsCount):
    if np.shape(aircraftPoints)[1] < 2  or stepsCount < 0:
        return missilePoints
    
    missileVelocity = missilePoints[:, -1] - missilePoints[:, -2]
   
    currentSightLine = aircraftPoints[:, 0] - missilePoints[:, -2]
    nextSightLine    = aircraftPoints[:, 1] - missilePoints[:, -1]
    
    currentDistance = np.linalg.norm(currentSightLine)

    if currentDistance <= 5:
        return missilePoints

    sightAngleDelta = angle(nextSightLine) \
                      - angle(currentSightLine)
    approachVelocity = abs(np.linalg.norm(nextSightLine) - currentDistance)

    angleLimit = np.pi / 36
    rotationAngle =  np.clip(k * approachVelocity * sightAngleDelta, -angleLimit, angleLimit)  
    nextMissileVelocity = rotate(missileVelocity, rotationAngle)
   
    nextPoint = missilePoints[:, -1] + nextMissileVelocity

    missilePoints = np.hstack((missilePoints, np.reshape(nextPoint, (2, 1)))) 

    return calcProportionalMissile(aircraftPoints[:, 1:], missilePoints, k, stepsCount - 1)

if __name__ == '__main__':
    if len(sys.argv) < 3 :
        print("Error not enough arguments!")
        print(__doc__)
        exit(1)

    calculateImitation(sys.argv[1], sys.argv[2])
    print('Done')
