import os
import sys
import datetime
from collections import defaultdict



data = defaultdict(int)
data19 = defaultdict(int)
with open('game_stats.txt', 'r') as f:
    date = None
    for line in f:
        items = line.split(',')
        newDate = items[5]
        date_time_obj = datetime.datetime.strptime(newDate, '%Y/%m/%d %H:%M:%S')
        newDate = str(date_time_obj.date())
        score = int(items[0])
        level = int(items[1])
        if level == 19:
            d = data19
        else:
            d = data            
        d[str(newDate)] = max(d[str(newDate)],score)

date_time_obj = datetime.datetime.strptime("2020/01/01 12:00:00", '%Y/%m/%d %H:%M:%S')
for i in range (365):
    key = str(date_time_obj.date())
    if key in data:
        print(key, (data[key]))
    else:
        print(key, str(0))
    date_time_obj += datetime.timedelta(days=1)
        
        

            