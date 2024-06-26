# Консольное приложение для фильтрации логов.
## Описание:
Считывает файл журнала доступа вида "1.3.0.0 : 2015-09-23 12:12:26" (IPv4 : дата время) и преобразует его в вид "1.3.0.0 - 1" (IPv4 - количество посещений).

## Команды:
### 1. ```filter``` - фильтрация данных
- Ключи:
```--file-log``` — путь к файлу с логами <br>
```--file-output``` — путь к файлу с результатом <br>
```--address-start``` —  нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса <br>
```--address-mask``` — маска подсети, задающая верхнюю границу диапазона. Десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start <br>
```--time-start``` —  нижняя граница временного интервала <br>
```--time-end``` — верхняя граница временного интервала
- Пример:
```
filter --file-log=log.txt --file-output=out.txt --address-start=1.0.0.0 \n --address-mask=24 --time-start=01.01.2010 --time-end=01.01.2024
```
### 2. ```generate``` - генерация тестовых данных
- Использование:
```generate filePath count``` <br>
*filePath* - путь к файлу, в котором будут храниться сгенерированные данные (создается если не существует) <br>
*count* - количество записей, которые необходимо сгенерировать
- Пример:
```generate data.txt 1000```
### 3. ```help``` - помощь (печатает доступные комманды с их описанием)
- Использование:
```help```
### 4. ```exit``` - Выход
- Использование:
```exit```
## Запуск:
- [x] ```dotnet run``` в директории проекта <br>
- [x] Запустить файл Test.exe (Скачать можно в разделе Releases)
