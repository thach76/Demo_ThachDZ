import math

def kiem_tra_so_nguyen_to(n):
    if n <= 1:
        return False
    for i in range(2, int(n**0.5) + 1):
        if n % i == 0:
            return False
    return True

def calculate_constants():
    constants = []
    dem = 0
    for i in range(311):
        prime = i + 2  # Số nguyên tố bắt đầu từ 2
        if kiem_tra_so_nguyen_to(prime) == False:
            continue
        can_bac_ba_prime = prime ** (1/3)  # Tính căn bậc 3 của prime
        fractional_part = can_bac_ba_prime - math.floor(can_bac_ba_prime)
        constant = int(fractional_part * (2**32))
        constants.append(hex(constant))
    return constants

constants = calculate_constants()

for i, constant in enumerate(constants):
    if i%8==0:
        print()
    print(constant + " " , end="")
    
