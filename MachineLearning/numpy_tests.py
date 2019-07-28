import unittest
import numpy as np

class TestTest(unittest.TestCase):
    def test_add(self):
        a = np.array([1, 2, 3])
        b = np.array([5, 3, 1])

        expected = np.array([6,5,4])
        actual = a + b

        self.assertTrue(np.array_equal(expected, actual))
    
    def test_commulative_add(self):
        a = np.array([15, 21, 34])
        b = np.array([52, 8, 71])

        self.assertTrue(np.array_equal(a + b, b + a))

    # n, m  x  m, o     =   n, o
    # 1 2 3    5  10    =   5 * 1 + 2 * 2 + 1 * 3       10 * 1 + 20 * 2 + 5 * 3
    # 4 5 6    2  20    =   5 * 4 + 2 * 5 + 1 * 6       10 * 4 + 20 * 5 + 5 * 6 
    #          1  5     =

    # 5  + 4  + 3   10 + 40  + 15   =   12   65    
    # 20 + 10 + 6   40 + 100 + 30   =   36  170

    def test_multiplication(self):
        a = np.array([[1, 2, 3], [4, 5, 6]]);
        b = np.array([[5, 10], [2, 20], [1, 5]]);

        expected = np.array([[12, 65], [36, 170]]);
        actual = np.matmul(a, b)

        self.assertTrue(np.array_equal(expected, actual))

    def test_non_commulative_multiplication(self):
        pass




if __name__ == '__main__':
    unittest.main()
