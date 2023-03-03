<?php
    session_start();

    // Zabezpieczenie przed wpisywaniem strony uzytkownika zalogowanego przez niezalogowane osoby
    if ( !isset($_SESSION['correct-login']) || $_SESSION['correct-login'] == false) {
        header('location: login.php');
        exit();
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Calendario - konto użytkownika</title>
</head>
<body>
    <h1>Użytkownik zalogowany</h1>

    <br>

    <form action="login_files/wyloguj.php" method="post">
        <input type="submit" value="Wyloguj się">
    </form>

    <?php
        // Sprawdzenie flagi is_remember
        if (($_SESSION['is_remember']) == 'on') {
            echo '<br><b>Użytkownik zapamiętany</b>';
        } else {
            echo '<br><b>Użytkownik nie zapamiętany</b>';
        }
    ?>
    
    <br><br>

    
</body>
</html>