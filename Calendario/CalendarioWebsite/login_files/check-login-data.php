<?php
    session_start();

    // Sprawdzenie czy użytkownik wpisał dane w formularzu logowania
    if (empty($_POST['login']) || empty($_POST['haslo'])) {
        $_SESSION['incorrectLoginData'] = true;
        header("location: ../login.php");
        exit();
    } else {
        $login = $_POST['login'];
        $password = $_POST['haslo'];
        $_SESSION['is_remember'] = $_POST['remember-me_checkbox'];
    }

    
    // CURL TEST
    require_once '../request.php';

    $requester = new HTTPRequester();
    echo "asd";
    echo $requester->login($login, $password);
?>