<?php
    session_start();

    $_SESSION['correct-login'] = false;

    session_unset();
    
    header('location: login.php');
    exit();

?>