import matplotlib.pyplot as plt
from Runge_Kutta4 import Runge_Kutta4
from Dormand_Prince5 import Dormand_Prince5
from Dormand_Prince8 import Dormand_Prince8
import numpy as np

def observer(t, y, ts, ys):
    ts.append(t)
    ys.append(y)

ts = []
ys = []

solver = Dormand_Prince8()
solver.initialize(
        [0],
        1,
        lambda t, y: [t],
        lambda t, y: t >= 100,
        lambda t, y: observer(t, y, ts, ys),
        e_pcnt=1e-15)

solver.solve(False)

plt.plot(ts, ys)
#plt.plot(ts, np.exp(ts), linestyle='dashed')
plt.grid()
plt.show()