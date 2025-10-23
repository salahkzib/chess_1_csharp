import pyautogui
import os

pyautogui.sleep(5)

w = 70
h = 70
q = 46
p = 56
lc = (123, 108)
x = lc[0]
y = lc[1]
r = []
i = 0
j = 0
while i < 8:
    while j < 8:
        loc_piece = (int((x+w/2-q/2)), int((y+h/2-p/2)))
        r.append(loc_piece)
        x+=(w+1)
        j+=1
    j = 0
    x = lc[0]
    y+=(h+1)
    i+=1
arr = []
n = 0
while n < 16:
    arr.append(r[n])
    n+=1
m = 47
while m < 64:
    arr.append(r[m])
    m+=1
k = 0
while k < 32:
    pyautogui.hotkey("ctrl", "v")
    pyautogui.sleep(1)
    pyautogui.click(1238,360)
    pyautogui.hotkey("ctrl", "a")
    l = tuple(str(arr[k]))
    j = l[1:(len(l)-1)]
    pyautogui.write(j)
    pyautogui.press("enter")
    pyautogui.click(863,389)
    k+=1

os.system("pause")
