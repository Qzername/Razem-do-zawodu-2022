<?php
    session_start();

    $login_register = $_POST['register-username-input'];
    $email_register = $_POST['register-email-input'];
    $passwrod1_register = $_POST['register-password1'];
    $passwrod2_register = $_POST['register-password2'];
    @$regulamin_checkbox_register = $_POST['regulamin-checkbox'];
    
    $correct_register;
    

    // Sprawdzaeie długości nicku w polu rejestracji
    if ( isset($login_register) && (strlen($login_register) > 20 || strlen($login_register) < 2) ) {
        $_SESSION['register-login-error'] = 'Nieprawidłowo wpisany login. Treść powinna zawierać od 3 do 20 znaków.';
        $correct_register = false;
    }

    // Sprawdzenie czy wpisany nick nie zawiera znaków alfanumerycznych
    if (!ctype_alnum($login_register) ) {
        $_SESSION['register-login-error'] = 'Nieprawidłowo wpisany login. Treśc powinna zawierać jedynie znaki alfanumeryczne.';
        $correct_register = false;
    }

    // Sprawdzenie poprawności emaila
    $email_registerB = filter_var($email_register, FILTER_SANITIZE_EMAIL);
    if ((filter_var($email_registerB, FILTER_VALIDATE_EMAIL) == false) || ($email_register != $email_registerB)) {
        $_SESSION['register-email-error'] = 'Nieprawidłowo wpisany email.';
        $correct_register = false;
    }

    // Sprawdzenie poprawności haseł
    $passwrod1_register = htmlspecialchars($passwrod1_register);
    $passwrod2_register = htmlspecialchars($passwrod2_register);
    if (strlen($passwrod1_register) < 8) {
        $_SESSION['register-password1-error'] = 'Nieprawidłowo wpisane hasło (min. 8 znaków).';
        $correct_register = false;
    }

    if ($passwrod1_register != $passwrod2_register) {
        $_SESSION['register-password2-error'] = 'Podane hasła nie są identyczne.';
        $correct_register = false;
    }
    
    // Sprawdzenie zaznaczenia checkboxa regulaminu
    if ($regulamin_checkbox_register != 'on') {
        $_SESSION['register-checkbox-error'] = 'Zaakceptuj regulamin';
        $correct_register = false;
    }

    // Czy wszystkie pola formularza zostały wypełnione poprawnie - jak nie przekieruj do register.php i pokaż błędy
    if (!isset($correct_register)) {
        // Poprawna rejestracja
        
        require_once 'create_account.php';
        create_account();
        
        $_SESSION['correct-register'] = true;
        header("location: ../login.php");
        exit();
    } else {
        // Niepoprawna rejestracja
        header("location: ../register.php");
        exit();
    }


?>