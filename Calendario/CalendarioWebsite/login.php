<?php
    session_start();

    $_SESSION['correct-login'] = false;
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Calendario</title>

    <!-- FONTS -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato:wght@300;700;900&display=swap" rel="stylesheet">


    <!-- CSS -->
    <link rel="stylesheet" href="css/login.css">
</head>
<body>
    <div class="container">
        
        <section class="navbar">
            <div class="navbar">
                <div class="navbar-logo">
                    <div class="logo">
                        <img src="./assets/images/navbar_logo.png" alt="navbar-logo">
                    </div>

                    <div class="logo-text">
                        <span class="logo-text-span">Calendario</span>
                    </div>
                </div>
            </div>
        </section>

        <?php
            if (isset($_SESSION['correct-register'])) {
                echo "<div class='correct-register'>
                    <span>Rejestracja zakończona pomyślnie</span>
                    <span>Zaloguj się</span>
                </div>";
                unset($_SESSION['correct-register']);
            }
        ?>
            
        <section class="login-container">
            <div class="login">
                <div class="login-nav">
                    <div class="login-button active">
                        <!-- <input type="button" value="Zaloguj się"> -->
                        <a href="login.php">Zaloguj się</a>
                    </div>

                    <div class="login-nav_line"></div>

                    <div class="register-button">
                        <!-- <input type="button" value="Zarejestruj się"> -->
                        <a href="register.php">Zarejestruj się</a>
                    </div>
                </div>    
            
                <div class="login-form">
                    <div class="sth-wrong">
                            <?php
                                if (isset($_SESSION['incorrectLoginData'])) {
                                    echo 'Podana nazwa użytkownika lub hasło jest niepoprawne';
                                    unset($_SESSION['incorrectLoginData']);
                                }

                            ?>
                        </div>

                    <form action="./login_files/check-login-data.php" method="post">
                        <div class="login-username">
                            <!-- <label for="login-username__form">Nazwa użytkownika</label> -->
                            <input type="text" name="login" id="login-username__form" placeholder="Login">
                        </div>

                        <div class="login-password">
                            <!-- <label for="login-password">Hasło</label> -->
                            <input type="password" name="haslo" id="login-password" placeholder="Hasło">
                        </div>

                        <div class="remember-me">
                            <input type="checkbox" name="remember-me_checkbox" id="remember-checkbox">
                            <label for="remember-checkbox">Zapamniętaj mnie</label>
                        </div>

                        <div class="login-submit">
                            <input type="submit" value="Zaloguj się">
                        </div>
                    </form>
                </div>
            </div>
            
        </section>

        <section class="footer"></section>
    
    </div>

</body>
</html>