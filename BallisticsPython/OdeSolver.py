import numpy as np
from typing import Callable
from typing import List
import matplotlib.pyplot as plt


class OdeSolver:

    def __init__(self,
                 c: List[float],
                 a: List[List[float]],
                 b: List[float], 
                 b_star: List[float] = None):
        
        self.c = np.array(c)
        self.a = np.array(a)
        self.b = np.array(b)
        self.b_star = np.array(b_star)
        

    def initialize(self, 
                   y0: List[float],
                   dt: float,
                   f: Callable[[float, List[float]], List[float]],
                   stop_func: Callable[[float, List[float]], bool],
                   observer_func: Callable[[float, List[float]], None],
                   t0=0,
                   observe_interval=0, 
                   e_pcnt: float = None,
                   dt_min: float = None,
                   dt_max: float = None):

        self.iteration = 0
        self.y = np.array(y0.copy())
        self.N = len(y0)
        self.t = t0
        self.t_observed = t0 - dt
        self.dt = dt
        self.f = f
        self.stop_func = stop_func
        self.observe = observer_func
        self.observe_interval = observe_interval
        self.e_pcnt = e_pcnt
        self.stopped = False

        if dt_min is None:
            self.dt_min = dt / 10
        else:
            self.dt_min = dt_min
        
        if dt_max is None:
            self.dt_max = dt * 10
        else:
            self.dt_max = dt_max

        self.observe(self.t, y0)

    def do_step(self, auto_step = False):
        
        if self.stopped:
            return False

        k = np.zeros((len(self.c), self.N))
        k[0, ] = np.array(self.f(self.t, self.y))

        for i, (c, a) in enumerate(zip(self.c[1:], self.a[1:]), start=1):

            ak = sum([np.multiply(ak, kj) for ak, kj in zip(a[:i], k[:i, ])])

            k[i, ] = np.array(self.f(self.t + c * self.dt, self.y + self.dt * ak))

        self.y = self.y + self.dt * sum([np.multiply(bi, ki) for bi, ki in zip(self.b, k)])

        self.iteration += 1
        self.t += self.dt

        if self.e_pcnt is not None and auto_step and self.b_star is not None:
            e = self.e_pcnt * self.y / 100
            s = sum([np.multiply(bsi - bi, ki) for bi, bsi, ki in zip(self.b, self.b_star, k)])
            h_array = np.abs(e / s)
            self.dt = np.clip(min(h_array), self.dt_min, self.dt_max) 

        if abs(self.t - self.t_observed) >= self.observe_interval:
            self.observe(self.t, self.y)
            self.t_observed = self.t

        if self.stop_func(self.t, self.y):
            self.stopped = True
            return False
        else: 
            return True
            


    def solve(self, auto_step = False):

        while self.do_step(auto_step):
            pass

    def solve2(self,
              y0: List[float],
              dt: float,
              f: Callable[[float, List[float]], List[float]],
              stop_func: Callable[[float, List[float]], bool],
              observer_func: Callable[[float, List[float]], None],
              t0=0,
              steps_to_observe=1,
              auto_step = False
              ):

        N = len(y0)
        y = np.array(y0.copy())
        t = t0
        iteration = 0
        observer_func(t, y)

        while not stop_func(t, y):
            k = np.zeros((len(self.c), N))
            k[0, ] = np.array(f(t, y))

            for i, (c, a) in enumerate(zip(self.c[1:], self.a[1:]), start=1):

                ak = sum([np.multiply(ak, kj) for ak, kj in zip(a[:i], k[:i, ])])

                k[i, ] = np.array(f(t + c * dt, y + dt * ak))

            y = y + dt * sum([np.multiply(bi, ki) for bi, ki in zip(self.b, k)])

            iteration += 1
            t += dt
            observer_func(t, y)

