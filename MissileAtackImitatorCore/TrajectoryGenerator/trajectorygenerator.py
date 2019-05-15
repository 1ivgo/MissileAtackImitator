"""Imitation calculation module.

Usage:
python3 trajectorygenerator.py requestFileName responseFileName
"""

import sys
import os
import json
from bezier import Curve
import numpy as np
from numpy import linalg
from missile import Missile
import linal as l

def calculateImitation(requestFileName, responseFileName):
    requestFile = open(requestFileName)
    request = json.load(requestFile)
    
    stepsCount = request['StepsCount']

    curvesBasisPoints = np.hstack(tuple(map(requestPointToNPPoint, request['AircraftPoints'])))

    at = calculateAircraftTrajectory(curvesBasisPoints, stepsCount)
    response = {'AircraftTrajectory' : list(map(npPointToResponsePoint, 
                                                np.hsplit(at, np.shape(at)[1])))}
    
    missile = request['Missile']
    
    m1 = Missile()
    m1.stepsCount = stepsCount
    m1.launchPoint = requestPointToNPPoint(missile['LaunchPoint'])
    direction = requestPointToNPPoint(missile['Direction']) - m1.launchPoint
    m1.startVelocity = l.unitVector(direction) * missile['VelocityModule']
    m1.k = 20

    m2 = m1.copy()
    m2.k = 1

    mt = m1.trajectory(at)
    fuzzyMt = m2.trajectory(at)
    
    response['MissileTrajectory'] = list(map(npPointToResponsePoint, 
                                                np.hsplit(mt, np.shape(mt)[1])))

    response['FuzzyMissileTrajectory'] = list(map(npPointToResponsePoint, 
                                                np.hsplit(fuzzyMt, np.shape(fuzzyMt)[1])))                                           

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

if __name__ == '__main__':
    if len(sys.argv) < 3 :
        print("Error not enough arguments!")
        print(__doc__)
        exit(1)

    calculateImitation(sys.argv[1], sys.argv[2])
    print('Done')
