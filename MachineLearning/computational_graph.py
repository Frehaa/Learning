import numpy as np

def add(a, b):
    return lambda : a() + b()

def multiply(a, b):
    return lambda: np.matmul(a(), b())

