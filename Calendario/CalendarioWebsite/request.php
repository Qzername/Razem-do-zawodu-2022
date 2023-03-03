<?php
    class HTTPRequester {

        function __construct() {
            $this->startSession(); 

            $this->ip = null;
            $this->port = null;
            $this->token = null;
            $this->tokenExpiration = null;
            
            // $_SESSION['token'];
            
            
            $this->setServerData();
        }

        private function startSession() {
            if (session_status() == 'PHP_SESSION_DISABLED') {
                session_start();
            }
            
        }

        private function setServerData() {
            require_once 'server/server.php';
            $this->ip = $ip_adress;
            $this->port = $port_num;
        }

        // sprawdzenie poprawności tokena
        private function checkToken() {

        }


        public function login($login, $password) {
            // POST 20.25.191.186:6969/api/Login
            // {
            //     "Login": "LOGIN", (string)
            //     "Password": "HASLO" (string)
            // }
            $loginUrl = $this->ip."/api/Account/Login";

            // $loginData = http_build_query(array("Login" => "$login", "Password" => "$password"));
            $jsonSring =  json_encode(["Login" => "$login", "Password" => "$password"]);

            $ch = curl_init();

            curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
            curl_setopt($ch, CURLOPT_URL, $loginUrl);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'Content-Length:'.strlen($jsonSring)
            ));
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_POST, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonSring);

            $response = curl_exec($ch);

            

            // echo "<pre>";
            // print_r(curl_getinfo($ch));
            // echo "</pre>";

            curl_close($ch);

            if (empty($response)) {
                printf("<br> <br>Nothing returned from url");
            } else {
                if (strpos($response, 'Bad Request')) {
                    $_SESSION['incorrectLoginData'] = true;
                    header('location: ../login.php');
                    exit();
                }


                // Wytnij oraz zapisz token, date wygasniecia token'a
                if ($id = strpos($response, '{')) {
                    $returnedJson = substr($response, $id);
                }
    
                $returnedArray = json_decode($returnedJson, true);
                
                $this->token = $returnedArray['package'];
                $this->tokenExpiration = $returnedArray['expiration'];

                $_SESSION['token'] = $returnedArray['package'];
            }

            $_SESSION['correct-login'] = true;
            header("location: ../zalogowany.php");
            exit();
            
        }

        public function register($login, $password) {
            // POST 20.25.191.186:6969/api/Register
            // {
            //     "Login": "LOGIN", (string)
            //     "Password": "HASLO" (string)
            // }  
            $registerUrl = $this->ip.'/api/Account/Register';

            // $registerdata = http_build_query(array("Login" => "$login", "Password" => "$password"));
            $jsonSring =  json_encode(["Login" => "$login", "Password" => "$password"]);

            $ch = curl_init();

            curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
            curl_setopt($ch, CURLOPT_URL, $registerUrl);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'Content-Length:'.strlen($jsonSring)
            ));
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_POST, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonSring);

            $response = curl_exec($ch);

            // echo "<pre>";
            // print_r(curl_getinfo($ch));
            // echo "</pre>";

            curl_close($ch);

            if (empty($response)) {
                printf("<br> <br>Nothing returned from url");
            } else {
                return printf($response);
    

            }

        } 

        public function getTasks() {
            // GET 20.25.191.186:6969/api/Task

            $getTaskURL = $this->ip.'/api/Task?';

            $ch = curl_init();

            curl_setopt($ch, CURLOPT_URL, $getTaskURL);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'token: '.$_SESSION['token']
            ));
            
            $response = curl_exec($ch);

            // echo "<pre>";
            // print_r(curl_getinfo($ch));
            // echo "</pre>";

            curl_close($ch);

            if (empty($response)) {
                printf("<br> <br>Nothing returned from url");
            } else {
                echo "<pre>";
                print_r('<br />'.$response);
                echo "</pre>";
    

            }

 
        }

        public function getTask($taskId) {
            // GET 20.25.191.186:6969/api/Task/WSTAW_TUTAJ_ID

            $getTaskURL = $this->ip.'/api/Task?'.$taskId;

            $ch = curl_init();

            curl_setopt($ch, CURLOPT_URL, $getTaskURL);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'token: '.$_SESSION['token']
            ));
            
            $response = curl_exec($ch);

            // echo "<pre>";
            // print_r(curl_getinfo($ch));
            // echo "</pre>";

            curl_close($ch);

            if (empty($response)) {
                printf("<br> <br>Nothing returned from url");
            } else {
                echo "<pre>";
                print_r('<br />'.$response);
                echo "</pre>";
    

            }
        }

        public function createTask($taskName, $taskDescription, $taskPriorityID) {
            // POST 20.25.191.186:6969/api/Task
            // {
            //     "Name": "NAZWA", (string)
            //     "Description": "OPIS", (string)
            //     "PriorityID": np. 10 (int)
            // }

            $createTaskUrl = $this->ip.'/api/Task';

            // $registerdata = http_build_query(array("Login" => "$login", "Password" => "$password"));
            $jsonSring =  json_encode(["Name" => "$taskName", "Description" => "$taskDescription", "PriorityID" => "$taskPriorityID"]);

            $ch = curl_init();

            curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
            curl_setopt($ch, CURLOPT_URL, $createTaskUrl);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'Content-Length:'.strlen($jsonSring),
                'token: '.$_SESSION['token']
            ));
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_POST, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonSring);

            $response = curl_exec($ch);

            // echo "<pre>";
            // print_r(curl_getinfo($ch));
            // echo "</pre>";

            curl_close($ch);

            if (empty($response)) {
                printf("<br> <br>Nothing returned from url");
            } else {
                return printf($response);
    

            }
        }

        public function deletTask($taskId) {
            // DELETE 20.25.191.186:6969/api/Task/WSTAW_TUTAJ_ID

            
        }

        public function getPriorities() {
            // GET 20.25.191.186:6969/api/Priority


        }

        public function getPriority($priorityId) {
            // GET 20.25.191.186:6969/api/Priority/WSTAW_TUTAJ_ID
            

        }

        public function createPriority($priorityName, $colorHex) {
            // POST 20.25.191.186:6969/api/Priority
            // {
            //     "Name": "NAZWA", (string)
            //     "ColorHex": np. "#00ff44" (string)
            // }


        }

        public function deletePriority($priorityId) {
            // DELETE 20.25.191.186:6969/api/Priority/WSTAW_TUTAJ_ID


        }

        public function getSchedule($scheduleId) {
            // GET 20.25.191.186:6969/api/Schedule/WSTAW_TUTAJ_ID


        }

        public function createSchedule($taskId, $dateBegin, $dateEnd, $dateRemind) {
            // POST 20.25.191.186:6969/api/Schedule
            // {
            //     "TaskID": np. 5, (int)
            //     "DateBegin": np. 2525324321423412312, (long)
            //     "DateEnd": np. 2525324321423412312, (long)
            //     "DateRemind": np. 2525324321423412312 (long)
            // }


        }

        public function deleteShedule($scheduleId) {
            // DELETE 20.25.191.186:6969/api/Schedule/WSTAW_TUTAJ_ID
        }
    }
?>