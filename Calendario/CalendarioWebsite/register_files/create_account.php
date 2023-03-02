<?php
    function create_account() {
        // połączenie z bazą danych
        require_once '../mysql/connect.php';
        
        $db_connect = new mysqli($host, $db_user, $db_password, $db_name);

        // Nieprawidłowe podłączenie do bazy dancyh
        if ($db_connect->connect_errno != 0) {
            echo "Błąd bazy danych";
            exit();
        }

        $sql_ilu_userow = 'SELECT * FROM users WHERE ID = (SELECT MAX(ID) from users)';
        if ($rezultat_ilu_userow = $db_connect->query($sql_ilu_userow)) {
            $ilu_userow = $rezultat_ilu_userow->fetch_assoc();
            echo $ilu_userow['ID'];
            exit();
        }

        $sql = "INSERT INTO `users`(`ID`, `login`, `email`, `haslo`) VALUES ('[value-1]','[value-2]','[value-3]','[value-4]')";
     }
?>