a = open('data.json').read()

with open('data_replaced_k.json', 'w') as b:
    b.write(a.replace('ک',  'ك'))

with open('data_replaced_y.json', 'w') as b:
    b.write(a.replace('ی',  'ي'))

with open('data_replaced_ky.json', 'w') as b:
    b.write(a.replace('ک',  'ك').replace('ی', 'ي'))