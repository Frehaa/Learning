import unittest
import numpy as np

class TestTest(unittest.TestCase):
    def test_add(self):
        a = np.array([1, 2, 3])
        b = np.array([5, 3, 1])

        expected = np.array([6,5,4])
        actual = a + b

        self.assertTrue(np.array_equal(expected, actual))
    
    def test_commulative(self):
        a = np.array([15, 21, 34])
        b = np.array([52, 8, 71])

        self.assertTrue(np.array_equal(a + b, b + a))

    def test_one(self):
        pass

if __name__ == '__main__':
    unittest.main()
