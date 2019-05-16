import numpy as np
from numpy import linalg

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