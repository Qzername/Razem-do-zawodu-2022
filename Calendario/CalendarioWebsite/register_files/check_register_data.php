<?php
    include '../request.php';

    session_start();

    $login_register = $_POST['register-username-input'];
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
        $_SESSION['register-login-error'] = 'Nieprawidłowo wpisany login.';
        $correct_register = false;
    }

    // Sprawdzenie poprawności haseł
    $passwrod1_register = htmlspecialchars($passwrod1_register);
    $passwrod2_register = htmlspecialchars($passwrod2_register);
    if (!preg_match('/^(?=\S{8,})(?=\S*[a-z])(?=\S*[A-Z])(?=\S*[\W])(?=\S*[\d])\S*$/', $passwrod1_register)) {
        $_SESSION['register-password1-error'] = 'Nieprawidłowo wpisane hasło (min. 8 znaków, mała litera, duża litera, znak specjalny, liczba).';
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
        
        // Utworzenie uzytkownika

        $request = new HTTPRequester;
        $request->register($login_register, $passwrod1_register);
        
        // $_SESSION['correct-register'] = true;
        // header("location: ../login.php");
        // exit();

    } else {
        // Niepoprawna rejestracja
        header("location: ../register.php");
        exit();
    }


?>

<!-- 
    
/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/
^\S*(?=\S{8,20})(?=\S*[a-z])(?=\S*[A-Z])(?=\S*[\d])(?=\S*[\W])\S*$^ 

-->