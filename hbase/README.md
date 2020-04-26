# HBase

## Exercício 1 - Aquecendo com alguns dado

**1. Crie a tabela com 2 famílias de colunas:**
***a. personal-data***
***b. professional-data***
```
hbase(main):033:0> create 'italians', 'personal-data', 'professional-data'
Created table italians
Took 1.2631 seconds
=> Hbase::Table - italians
```

**2. Importe o arquivo via linha de comando**
```
hbase shell /tmp/italians.txt
2020-04-26 20:14:04,519 WARN  [main] util.NativeCodeLoader: Unable to load native-hadoop library for your platform... using builtin-java classes where applicable
Took 0.9583 seconds
Took 0.0040 seconds
Took 0.0032 seconds
...
Took 0.0043 seconds
Took 0.0076 seconds
Took 0.0034 seconds
Took 0.0029 seconds
HBase Shell
Use "help" to get list of supported commands.
Use "exit" to quit this interactive shell.
For Reference, please visit: http://hbase.apache.org/2.0/book.html#shell
Version 2.1.2, r1dfc418f77801fbfb59a125756891b9100c1fc6d, Sun Dec 30 21:45:09 PST 2018
Took 0.0028 seconds
```

## Exercício 2

**1. Adicione mais 2 italianos mantendo adicionando informações como data de nascimento nas informações pessoais e um atributo de anos de experiência nas informações profissionais**
```
hbase(main):036:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari'
Took 0.0252 seconds
hbase(main):037:0> put 'italians', '11', 'personal-data:city', 'Verona'
Took 0.0088 seconds
hbase(main):038:0> put 'italians', '11', 'professional-data:role', 'Desenvolvimento de Software'
Took 0.0022 seconds
hbase(main):039:0> put 'italians', '11', 'professional-data:salary', '5879'
Took 0.0097 seconds
hbase(main):040:0> put 'italians', '12', 'personal-data:name', 'Chiara Lombardo'
Took 0.0046 seconds
hbase(main):041:0> put 'italians', '12', 'personal-data:city', 'Rome'
Took 0.0035 seconds
hbase(main):042:0> put 'italians', '12', 'professional-data:role', 'Gerente de Projetos'
Took 0.0043 seconds
hbase(main):043:0> put 'italians', '12', 'professional-data:salary', '6200'
Took 0.0040 seconds
```

**2. Adicione o controle de 5 versões na tabela de dados pessoais**
```
hbase(main):044:0> alter 'italians', NAME => 'personal-data', VERSIONS => 5
Updating all regions with the new schema...
1/1 regions updated.
Done.
Took 2.7428 seconds
```

**3. Faça 5 alterações em um dos italianos**
```
hbase(main):076:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari 1'
Took 0.0048 seconds
hbase(main):077:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari 2'
Took 0.0021 seconds
hbase(main):078:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari 3'
Took 0.0070 seconds
hbase(main):079:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari 4'
Took 0.0026 seconds
hbase(main):080:0> put 'italians', '11', 'personal-data:name', 'Mario Ferrari 5'
Took 0.0186 seconds
```


**4. Com o operador get, verifique como o HBase armazenou o histórico**
```
hbase(main):081:0> get 'italians', '11', {COLUMN => 'personal-data:name', VERSIONS => 5}
COLUMN                                   CELL
 personal-data:name                      timestamp=1587936897030, value=Mario Ferrari 5
 personal-data:name                      timestamp=1587936896128, value=Mario Ferrari 4
 personal-data:name                      timestamp=1587936896097, value=Mario Ferrari 3
 personal-data:name                      timestamp=1587936896057, value=Mario Ferrari 2
 personal-data:name                      timestamp=1587936896025, value=Mario Ferrari 1
1 row(s)
Took 0.0195 seconds
```

**5. Utilize o scan para mostrar apenas o nome e profissão dos italianos**
```
hbase(main):082:0> scan 'italians', {COLUMNS => ['personal-data:name', 'professional-data:role']}
ROW                                      COLUMN+CELL
 1                                       column=personal-data:name, timestamp=1587936770437, value=Paolo Sorrentino
 1                                       column=professional-data:role, timestamp=1587936770527, value=Gestao Comercial
 10                                      column=personal-data:name, timestamp=1587936770798, value=Giovanna Caputo
 10                                      column=professional-data:role, timestamp=1587936770810, value=Comunicacao Institucional
 11                                      column=personal-data:name, timestamp=1587936897030, value=Mario Ferrari 5
 11                                      column=professional-data:role, timestamp=1587936826787, value=Desenvolvimento de Software
 12                                      column=personal-data:name, timestamp=1587936826852, value=Chiara Lombardo
 12                                      column=professional-data:role, timestamp=1587936826935, value=Gerente de Projetos
 2                                       column=personal-data:name, timestamp=1587936770552, value=Domenico Barbieri
 2                                       column=professional-data:role, timestamp=1587936770572, value=Psicopedagogia
 3                                       column=personal-data:name, timestamp=1587936770592, value=Maria Parisi
 3                                       column=professional-data:role, timestamp=1587936770611, value=Optometria
 4                                       column=personal-data:name, timestamp=1587936770624, value=Silvia Gallo
 4                                       column=professional-data:role, timestamp=1587936770640, value=Engenharia Industrial Madeireira
 5                                       column=personal-data:name, timestamp=1587936770666, value=Rosa Donati
 5                                       column=professional-data:role, timestamp=1587936770682, value=Mecatronica Industrial
 6                                       column=personal-data:name, timestamp=1587936770695, value=Simone Lombardo
 6                                       column=professional-data:role, timestamp=1587936770707, value=Biotecnologia e Bioquimica
 7                                       column=personal-data:name, timestamp=1587936770718, value=Barbara Ferretti
 7                                       column=professional-data:role, timestamp=1587936770732, value=Libras
 8                                       column=personal-data:name, timestamp=1587936770746, value=Simone Ferrara
 8                                       column=professional-data:role, timestamp=1587936770758, value=Engenharia de Minas
 9                                       column=personal-data:name, timestamp=1587936770775, value=Vincenzo Giordano
 9                                       column=professional-data:role, timestamp=1587936770786, value=Marketing
12 row(s)
Took 0.0665 seconds
```

**6. Apague os italianos com row id ímpar**
```
hbase(main):083:0> deleteall 'italians', '1'
Took 0.0030 seconds
hbase(main):084:0> deleteall 'italians', '3'
Took 0.0035 seconds
hbase(main):085:0> deleteall 'italians', '5'
Took 0.0028 seconds
hbase(main):086:0> deleteall 'italians', '7'
Took 0.0019 seconds
hbase(main):087:0> deleteall 'italians', '9'
Took 0.0042 seconds
hbase(main):088:0> deleteall 'italians', '11'
Took 0.0059 seconds
```

```
hbase(main):089:0> scan 'italians', {COLUMNS => ['personal-data:name', 'professional-data:role']}
ROW                                      COLUMN+CELL
 10                                      column=personal-data:name, timestamp=1587936770798, value=Giovanna Caputo
 10                                      column=professional-data:role, timestamp=1587936770810, value=Comunicacao Institucional
 12                                      column=personal-data:name, timestamp=1587936826852, value=Chiara Lombardo
 12                                      column=professional-data:role, timestamp=1587936826935, value=Gerente de Projetos
 2                                       column=personal-data:name, timestamp=1587936770552, value=Domenico Barbieri
 2                                       column=professional-data:role, timestamp=1587936770572, value=Psicopedagogia
 4                                       column=personal-data:name, timestamp=1587936770624, value=Silvia Gallo
 4                                       column=professional-data:role, timestamp=1587936770640, value=Engenharia Industrial Madeireira
 6                                       column=personal-data:name, timestamp=1587936770695, value=Simone Lombardo
 6                                       column=professional-data:role, timestamp=1587936770707, value=Biotecnologia e Bioquimica
 8                                       column=personal-data:name, timestamp=1587936770746, value=Simone Ferrara
 8                                       column=professional-data:role, timestamp=1587936770758, value=Engenharia de Minas
6 row(s)
Took 0.0435 seconds
```

**7. Crie um contador de idade 55 para o italiano de row id 5**
```
hbase(main):090:0> incr 'italians', '2', 'personal-data:age', 55
COUNTER VALUE = 55
Took 0.0095 seconds
```

**8. Incremente a idade do italiano em 1**
```
hbase(main):091:0> incr 'italians', '2', 'personal-data:age'
COUNTER VALUE = 56
Took 0.0032 seconds
```