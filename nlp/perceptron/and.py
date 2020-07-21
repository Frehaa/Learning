from numpy import dot, array
from numpy.random import rand, choice, randint

def unit_step(x): 
    return 1 if x > 0 else 0

w = rand(3)

training_data = [
        (array([0,0,1]), 0),
        (array([0,1,1]), 0),
        (array([1,0,1]), 0),
        (array([1,1,1]), 1),
        ]

eta = 0.2
n = 100
errors = []

for _ in range(n):
    x, expected = training_data[randint(len(training_data))]
    result = dot(x, w)
    output = unit_step(result)
    w = w - eta * (output - expected) * x
    print(expected, output, w)

 

