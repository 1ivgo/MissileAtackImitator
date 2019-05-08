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


def calculateImitation(requestFileName, responseFileName):
    requestFile = open(requestFileName)
    request = json.load(requestFile)
    
    curvesBasisPoints = np.hstack(tuple(map(requestPointToNPPoint, request['AircraftPoints'])))

    # import pdb; pdb.set_trace()

    curves = npPointsToCurves(curvesBasisPoints, 3)

    evaluate = lambda curve: \
        curve.evaluate_multi(np.linspace(0.0, 1.0, request['StepsCount'] // len(curves)))
   
    t = np.hstack(tuple(map(evaluate, curves)))
    response = {'AircraftTrajectory' : list(map(npPointToResponsePoint, 
                                                np.hsplit(t, np.shape(t)[1])))}
    
    json.dump(response, open(responseFileName, 'w'))
    return 

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
