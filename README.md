# CScheduler
1. Сайт проекта http://193.124.59.193
2. Проект позволяет вызывать указанные функции в смарт контрактах в указанное время
3. Доступ ко всем функциям сайта имеет только зарегистрированный пользователь.

Пример использования:
Для правильного функционирования работы сайта http://creditsevents.com необходимо проводить завершение раунда. 

После завершения раунда происходит следующее:
- Среди участников распределяется вознаграждение
- Текущий опрос завершается
- Начинается новый опрос

Для реализации завершения раунда в смарт контракте предусмотрен метод executeRound(). Для автоматического вызова этого метода можно использовать данный сервис. Для этого необходимо сделать следующее:
- Перейти на сайт http://193.124.59.193 и зарегистрироваться
- На главой странице нажать кнопку "Добавить задачу" и заполнить все параметры задачи. 

# API
Для программного добавления задач можно использовать API по адресу http://193.124.59.193/Api/AddNewTask
Список параметров:
- apiKey (обязательный) - ваш API ключ. При регистрации на сайте http://193.124.59.193 вам автоматически присваивается уникальный ключ. Найти ключ можно в личном кабинете (Menu - My account - Personal data)
- name (обязательный) - наименование задачи. Тип строка.
- network (обязательный) - сеть, в которой находится ваш смарт контракт. Тип строка. Может принимать одно из трех значений: "CreditsNetwork", "DevsDappsTestnet" или "testnet-r4_2".
- method (обязательный) - публичный метод в смарт контракте, который вы собираетесь вызывать в запланированное время. Тип строка. Например: "executeRound". Указывается без скобок.
- address (обязательный) - адрес смарт контракта. Тип строка. Например: "GVGAFSYAsTSfnnAZuHzHL43q9UpbvpEZzKn2VmfaMcEH".
- executionMode (обязательный) - периодичность с которой будет вызываться вышеуказанные метод (method) в смарт контракте. Тип строка. Может принимать одно из трех значений:
    - "Regular" - задача будет выполняться регулярно
    - "Once" - задача будет выполнена единожды в строго указанное время
    - "CronExpression" - выражение в формате Cron. Например: "0,11 0,2,34 0,15 6 APR ? *". Данное выражение можно сформировать автоматически, используя какой-либо онлайн-сервис, например, этот https://www.freeformatter.com/cron-expression-generator-quartz.html

Заполнение других параметров зависят от того какой executionMode указан.</br>
Если executionMode="Regular", то необходимо передать еще 4 параметра:
    <ul>
    <li>"regularDateFrom" - дата начала выполнения в формате "ММ-ДД-ГГГГ-ЧЧ-ММ-СС". Тип строка.</li>
    <li>"regularDateTo" - дата окончания выполнения в формате "ММ-ДД-ГГГГ-ЧЧ-ММ-СС". Тип строка.</li>
    <li>"regularPeriod" - периодичность. Может принимать 1 из трех значений: "Days", "Hours" или "Minutes". Тип строка.</li>
    <li>"regularValue" - частота выполнения. Тип строка. Целочисленное значение. Например: 1, 3, 5, 10.</li>
    </ul>
Если executionMode="Once", то необходимо передать еще 1 параметр:
    <ul>
    <li>"onceDate" - дата выполнения в формате "ММ-ДД-ГГГГ-ЧЧ-ММ-СС"</li>
    </ul>
Если executionMode="CronExpression", то необходимо передать еще 1 параметр:
    <ul>
    <li>"cronExpression" - выражение в формате Cron</li>
    </ul>
    
Пример 1. Метод executeRound в смарт контракте по адресу GVGAFSYAsTSfnnAZuHzHL43q9UpbvpEZzKn2VmfaMcEH будет вызываться с 1-го января по 31-е декабря каждые 3 часа.
http://193.124.59.193/Api/AddNewTask?apiKey="YourApiKey"&name="Test1"&network="DevsDappsTestnet"&method="executeRound"&address="GVGAFSYAsTSfnnAZuHzHL43q9UpbvpEZzKn2VmfaMcEH"&executionMode="Regular"&regularDateFrom="01-01-2019-01-01-01"&regularDateTo="12-31-2019-23-59-59"&regularPeriod="Hours"&regularValue="3"

Пример 2. Метод executeRound будет вызван единожды 31 декабря в 23:59:59
http://193.124.59.193/Api/AddNewTask?apiKey="YourApiKey"&name="Test2"&network="testnet-r4_2"&method="executeRound"&address="GVGAFSYAsTSfnnAZuHzHL43q9UpbvpEZzKn2VmfaMcEH"&executionMode="Once"&onceDate="12-31-2019-23-59-59"

Пример 3. Метод executeRound будет вызываться согласно расписанию, закодированному в формате cron
http://193.124.59.193/Api/AddNewTask?apiKey="YourApiKey"&name="Test3"&network="CreditsNetwork"&method="executeRound"&address="GVGAFSYAsTSfnnAZuHzHL43q9UpbvpEZzKn2VmfaMcEH"&executionMode="CronExpression"&cronExpression="0,11 0,2,34 0,15 6 APR ? *"
