from OdeSolver import OdeSolver

class Runge_Kutta4(OdeSolver):

    def __init__(self):

        super().__init__(
            [0, 1/2, 1/2, 1],
            [
            [0, 0, 0, 0],
            [1/2, 0, 0, 0],
            [0, 1/2, 0, 0],
            [0, 0, 1, 0],
            ],
            [1/6, 1/3, 1/3, 1/6])
