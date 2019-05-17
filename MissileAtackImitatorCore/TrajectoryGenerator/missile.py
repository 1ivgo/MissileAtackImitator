import numpy as np
from numpy import linalg
import linal as l
import copy

class Missile:
    def __init__(self):
        self.stepsCount = None
        self.launchPoint = None
        self.startVelocity = None
        self.k = None
        self.angleLimit = np.pi / 36
        self.hasHit = False

    def copy(self):
        return copy.deepcopy(self)

    def trajectory(self, aircraftPoints):
        self.velocity = self.startVelocity
        self.points = np.hstack((self.launchPoint, self.launchPoint + self.velocity))

        return self.calcProportionalMissile(aircraftPoints)

    def calcProportionalMissile(self, aircraftPoints):
        if np.shape(aircraftPoints)[1] < 2  or self.stepsCount < 0:
            return self.points
        
        self.sightLine   = aircraftPoints[:, 0] - self.points[:, -2]
        nextSightLine    = aircraftPoints[:, 1] - self.points[:, -1]
        
        currentDistance = np.linalg.norm(self.sightLine)

        if currentDistance <= 5:
            self.hasHit = True
            return self.points

        sightAngleDelta = l.angle(nextSightLine) \
                        - l.angle(self.sightLine)
        approachVelocity = abs(np.linalg.norm(nextSightLine) - currentDistance)

        rotationAngle =  np.clip(self.k * approachVelocity * sightAngleDelta, 
                                -self.angleLimit, self.angleLimit)  
        self.velocity = l.rotate(self.velocity, rotationAngle)
    
        nextPoint = np.reshape(self.points[:, -1], (2, 1)) + self.velocity
        self.points = np.hstack((self.points, nextPoint)) 

        self.sightLine = nextSightLine
        return self.calcProportionalMissile(aircraftPoints[:, 1:])