<?php
    session_start();

    // Sprawdzenie czy użytkownik wpisał dane w formularzu logowania
    if (empty($_POST['login']) || empty($_POST['haslo'])) {
        header("location: ../login.php");
        exit();
    } else {
        $login = htmlspecialchars($_POST['login']);
        $password = htmlspecialchars($_POST['haslo']);
    }

    
    // CURL TEST
    require_once '../request.php';

    $requester = new HTTPRequester();
    echo $requester->login($login, $password);
?>