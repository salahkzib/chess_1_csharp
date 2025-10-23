import pyautogui
import os

pyautogui.sleep(5)
#375,665

x = 375
y = 665
n = 0
i = 0
while n<8:
    j = 0
    while j<8:
        pyautogui.click(x, y)
        pyautogui.sleep(0.3)
        pyautogui.write(f"{j}")
        pyautogui.press("enter")
        pyautogui.sleep(0.3)
        y-=72
        j+=1
    y = 665
    x+=70
    i+=1
    n+=1



