<?php
    require "server.php";


    class HTTPRequester {
         function __construct(){
            $this->port = $port_num;
            $this->ip = $ip_adress;
            // $this->token = $_SESSION['token'];
        

        }

        public static function HTTPPost($url, $jsonString) {
            $ch = curl_init();
            curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
            curl_setopt($ch, CURLOPT_URL, $url);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'Content-Length:'.strlen($jsonSring),
                'token: '.$this->token
            ));
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_POST, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonString);

            $response = curl_exec($ch);

            if (curl_errno($ch)) {
                $error_msg = curl_error($ch);
            }

            curl_close($ch);
            
            if (isset($error_msg)) {
                return "Serwer error: ".$error_msg;
            }

            return $response;
        }

        public static function HTTPGet($url, $jsonString) {
            $ch = curl_init();
            curl_setopt($ch, CURLOPT_URL, $url);
            curl_setopt($ch, CURLOPT_PORT, $this->port);
            curl_setopt($ch, CURLOPT_RESPONSETRANSFER, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonString);
            curl_setopt($ch, CURLOPT_HEADER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'token: '.$this->token
            ));

            $response = curl_exec($ch);

            if (curl_errno($ch)) {
                $error_msg = curl_error($ch);
            }

            curl_close($ch);

            if (isset($error_msg)) {
                return "Serwer error: ".$error_msg;
            }

            return $response;
        }


    }


?>
