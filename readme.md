POST 20.25.191.186:6969/api/Account/Login

{
	"Login": "LOGIN", (string)
	"Password": "HASLO" (string)
}

Logowanie - Przy pomyslnym logowaniu dostajesz spowrotem Token, ktory sluzy do weryfikacji przy robieniu kazdego zadania (usuwanie wydarzen itd.). Dodaj go do headerow.


POST 20.25.191.186:6969/api/Account/Register

{
	"Login": "LOGIN", (string)
	"Password": "HASLO" (string)
}

Rejestracja - Po pomyslnej rejestracji, zaloguj sie z tymi samymi danymi.




GET 20.25.191.186:6969/api/Task
(lub)
GET 20.25.191.186:6969/api/Task/WSTAW_TUTAJ_ID

Zwraca ci liste zadan/konkretne zadanie, ktore nalezy do zalogowanego konta.


POST 20.25.191.186:6969/api/Task

{
	"Name": "NAZWA", (string)
	"Description": "OPIS", (string)
	"PriorityID": np. 10 (int)
}

Tworzy zadanie (1 z 2 wymaganych rzeczy do stworzenia wydarzenia).


DELETE 20.25.191.186:6969/api/Task/WSTAW_TUTAJ_ID

Usuwa zadanie




GET 20.25.191.186:6969/api/Priority
(lub)
GET 20.25.191.186:6969/api/Priority/WSTAW_TUTAJ_ID

Zwraca ci liste priorytetow/konkretny priorytet, ktore nalezy do zalogowanego konta.


POST 20.25.191.186:6969/api/Priority

{
	"Name": "NAZWA", (string)
	"ColorHex": np. "#00ff44" (string)
}

Tworzy priorytet (1 z 2 wymaganych rzeczy do stworzenia wydarzenia).


DELETE 20.25.191.186:6969/api/Priority/WSTAW_TUTAJ_ID

Usuwa zadanie




GET 20.25.191.186:6969/api/Schedule/WSTAW_TUTAJ_ID

Zwraca ci konkretne wydarzenie, ktore nalezy do zalogowanego konta.


POST 20.25.191.186:6969/api/Schedule

{
	"TaskID": np. 5, (int)
	"DateBegin": np. 2525324321423412312, (long)
	"DateEnd": np. 2525324321423412312, (long)
	"DateRemind": np. 2525324321423412312 (long)
}

Tworzy wydarzenie. DateBegin to data rozpoczecia wydarzenia w tickach. DateEnd to data zakonczenia w tikach (moze byc null, serwer zamieni to w 0, czyli nigdy). DateRemind to data przypomnienia (wyslania powiadomienia) w tikach (moze byc null, serwer zamieni to w 0, czyli nigdy).
https://tickstodatetime.azurewebsites.net/


DELETE 20.25.191.186:6969/api/Schedule/WSTAW_TUTAJ_ID

Usuwa wydarzenie