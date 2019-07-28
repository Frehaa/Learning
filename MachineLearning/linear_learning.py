import numpy as np

a = 5
b = 0

examples = [(0, 2), (1, 5), (-3, -7)]
learning_rate = 0.01

# loss = ((ax + b) - y)^2
# loss = (ax + b - y)^2
# loss = (ax + b - y) * (ax + b - y) 
# loss = (ax)^2 + axb - axy + axb + b^2 - by - axy - by + y^2
# loss = a^2 * x^2 + axb - axy + axb + b^2 - by - axy - by + y^2
# loss = a^2 * x^2 + b^2 + y^2 + 2axb - 2axy - 2by 

# g_a = 2ax^2 + 2xb - 2xy
# g_b = 2b + 2ax - 2y

# (a + b)^2 = a^2 + b^2 + 2ab

for _ in range(50):
    losses = []
    for x, y in examples:
        value = a * x + b

        loss = (value - y)**2
        print(value, y, loss)

        gradient_a = 2*a*x**2 + 2*x*b - 2*x*y
        gradient_b = 2*b + 2*a*x - 2*y

        a -= gradient_a * learning_rate
        b -= gradient_b * learning_rate 
        
print()
print(a,b)
