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

import missile
import linal as l 

class TrajectoryGenerator:
    def __init__(self, requestFileName, responseFileName):
        self._request = json.load(open(requestFileName))
        self._response = {}
        
        self._stepsCount = self._request['StepsCount']
        self._aircraftTrajectory = None

        self._genAircraft()
        self._genMissiles()
    
        json.dump(self._response, open(responseFileName, 'w'))
        
    def _genAircraft(self):
        curvesBasisPoints = np.hstack(tuple(map(requestPointToNPPoint, 
                                                self._request['AircraftPoints'])))

        at = calculateAircraftTrajectory(curvesBasisPoints, self._stepsCount)
        self._response = {'AircraftTrajectory'
                          : list(map(npPointToResponsePoint, 
                                     np.hsplit(at, np.shape(at)[1])))}
        self._aircraftTrajectory = at

    def _genMissiles(self):
        settings = self._request['Missiles']
        
        usual = missile.Missile()
        usual.stepsCount = self._stepsCount
        usual.launchPoint = requestPointToNPPoint(settings['LaunchPoint'])
        direction = requestPointToNPPoint(settings['Direction']) - usual.launchPoint
        usual.startVelocity = l.unitVector(direction) * settings['VelocityModule']

        print(settings['PropCoeff'], file=open('log', 'a'))
        usual.controller = missile.controllers.Proportional(settings['PropCoeff'])

        fuzzy = usual.copy()
        fuzzy.controller = missile.controllers.Fuzzy(settings['Inference'], 
                                                     settings['Defuzzification'])

        ut = usual.trajectory(self._aircraftTrajectory)
        ut = list(map(npPointToResponsePoint, np.hsplit(ut, np.shape(ut)[1])))
        self._response['UsualMissile'] = {'Trajectory' : ut, 'IsHit' : usual.hasHit}

        ft = fuzzy.trajectory(self._aircraftTrajectory)
        ft = list(map(npPointToResponsePoint, np.hsplit(ft, np.shape(ft)[1])))                                           
        self._response['FuzzyMissile'] = {'Trajectory' : ft, 'IsHit' : fuzzy.hasHit}

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

    TrajectoryGenerator(sys.argv[1], sys.argv[2])
    print('Done')
