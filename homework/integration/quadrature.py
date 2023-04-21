import numpy as np
from scipy.integrate import quad

x = np.linspace(0, 1, 1000)

def integrand_sqrt(x):
    return np.sqrt(x)

def reciprocal_sqrt(x):
    return 1 / np.sqrt(x)

def ln_sqrt(x):
    return np.log(x) / np.sqrt(x)

def pi_approx(x):
    return 4 * np.sqrt(1-x**2)

def exponential_neg_x(x):
    return np.exp(-x)

def x_times_exp_neg_x_squared(x):
    return x * np.exp(-x**2)

def normal_distribution(x):
    return np.exp(-x**2 / 2) / np.sqrt(2 * np.pi)


integral_sqrt, error_sqrt = quad(integrand_sqrt, 0, 1)
integral_reciprocal_sqrt, error_reciprocal_sqrt = quad(reciprocal_sqrt, 0, 1)
integral_ln_sqrt, error_ln_sqrt = quad(ln_sqrt, 0, 1)
integral_pi_approx, error_pi_approx = quad(pi_approx, 0, 1)

integral_exp_neg_x, error_exp_neg_x = quad(exponential_neg_x, 0, np.inf)
integral_x_times_exp_neg_x_squared, error_x_times_exp_neg_x_squared = quad(x_times_exp_neg_x_squared, 0, np.inf)
integral_normal_distribution, error_normal_distribution = quad(normal_distribution, -np.inf, np.inf)

print(40 * "-", "python finite integrals using scipy", 40 * "-")
print("∫01 dx √(x) = ", integral_sqrt, "Error:", error_sqrt)
print("∫01 dx 1/√(x) = ", integral_reciprocal_sqrt, "Error:", error_reciprocal_sqrt)
print("∫01 dx ln(x)/√(x) = ", integral_ln_sqrt, "Error:", error_ln_sqrt)
print("∫01 dx 4√(1-x²) = ", integral_pi_approx, "Error:", error_pi_approx)


print(40 * "-", "python infinite  using scipy", 40 * "-")
print("∫0∞ dx e^(-x) = ", integral_exp_neg_x, "Error:", error_exp_neg_x)
print("∫0∞ dx x * e^(-x²) = ", integral_x_times_exp_neg_x_squared, "Error:", error_x_times_exp_neg_x_squared)
print("∫-∞^∞ dx e^(-x²/2) / √(2 * π) = ", integral_normal_distribution, "Error:", error_normal_distribution)



