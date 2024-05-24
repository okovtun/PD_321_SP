https://github.com/okovtun/PD_321_SP
https://www.youtube.com/playlist?list=PLeqyOOqxeiIP1SSzk8jj0fkal0XB05whX

https://empyreal96.github.io/nt-info-depot/Windows-Internals-PDFs/Windows%20System%20Internals%207e%20Part%201.pdf

TODO:
Написать диспетчер задач:
1. Обеспечить плавное обновление listViewProcesses;
2. Загрузить иконки для каждого процесса;
3. Добавить путь к файлу каждого процесса;
4. Сдлать сортировку по разным полям listViewProcesses;
5. Обеспечить добавление/уделение отдельных столбиков ListView;

TODO:
1. CLR via C# Chapter 22: CLR hosting and AppDomains;
2. При запуске 'UsingAppDomains' необходимые исполняемые файлы сами должны копироваться в нужную папку;

DONE:
1. Дописать обработчик кнопки 'Stop';	DONE
2. Вынести перемещение процесса между ListBox-ами в отдельную функцию;
3. Добавить кнопку 'Browse', которая позволяет выбрать директорию, из которой будут загружаться процессы;

DDE - Dynamic Data Exchange;
MMF - Memory Mapped Files;
ILcode

TODO:
1. Определить загрузку CPU для активного процесса;
2. Получить имя пользователя, от которого запущен процесс;	DONE
3. Получить имя файла активного процесса;					DONE
4. Разобраться с памятью;
5. Программа должна запускать несколько процессов, и все запущенные процессы должны отображаться через выпадающий список;