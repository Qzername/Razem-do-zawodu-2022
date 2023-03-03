<?php
    session_start();
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
    <link rel="stylesheet" href="css/register.css">
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
            // if ($_SESSION['correct-register']) {
            //     echo "UDANA REJESTRACJA";
            //     unset($_SESSION['correct-register']);
            // }
            // echo "HELLO";
        ?>
            
        <section class="register-container">
            <div class="register">
                <div class="register-nav">
                    <div class="login-button">
                        <!-- <input type="button" value="Zaloguj się"> -->
                        <a href="login.php">Zaloguj się</a>
                    </div>

                    <div class="register-nav_line"></div>

                    <div class="register-button">
                        <!-- <input type="button" value="Zarejestruj się"> -->
                        <a href="register.php">Zarejestruj się</a>
                    </div>
                </div>    
            
                <div class="register-form">
                    <form action="register_files/check_register_data.php" method="post">
                        <div class="register-username">
                            <div class="sth-wrong">
                                <!-- <span>Login lub hasło jest nieprawidłowe</span> -->
                                <?php
                                    if (isset($_SESSION['register-login-error'])) {
                                        echo $_SESSION['register-login-error'];
                                        unset($_SESSION['register-login-error']);
                                    }
                                ?>
                            </div>  
                            <!-- <label for="login-username__form">Nazwa użytkownika</label> -->
                            <input type="text" name="register-username-input" placeholder="Login">
                        </div>

                        <div class="register-password">
                            <div class="sth-wrong">
                                <?php 
                                    if (isset($_SESSION['register-password1-error'])) {
                                        echo $_SESSION['register-password1-error'];
                                        unset($_SESSION['register-password1-error']);
                                    }
                                ?>
                            </div>
                            <!-- <label for="login-password">Hasło</label> -->
                            <input type="password" name="register-password1" placeholder="Hasło">
                        </div>

                        <div class="register-password2">
                            <div class="sth-wrong">
                                <?php 
                                    if (isset($_SESSION['register-password2-error'])) {
                                        echo $_SESSION['register-password2-error'];
                                        unset($_SESSION['register-password2-error']);
                                    }
                                ?>
                            </div>
                            <!-- <label for="login-password">Hasło</label> -->
                            <input type="password" name="register-password2" placeholder="Powtórz hasło">
                        </div>

                        <div class="sth-wrong">
                                <?php 
                                    if (isset($_SESSION['register-checkbox-error'])) {
                                        echo $_SESSION['register-checkbox-error'];
                                        unset($_SESSION['register-checkbox-error']);
                                    }
                                ?>
                            </div>
                            
                        <div class="regulamin">
                            <input type="checkbox" name="regulamin-checkbox" id="regulamin-checkbox">
                            <label for="regulamin-checkbox">Potwierdzam warunki regulaminu</label>
                        </div>

                        <div class="register-submit">
                            <input type="submit" value="Zarejestruj się">
                        </div>
                    </form>
                </div>

            </div>
            
        </section>

        <section class="footer"></section>
    
    </div>

</body>
</html>