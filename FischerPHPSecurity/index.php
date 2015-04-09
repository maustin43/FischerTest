<?php include "FischerEncryption.php"; ?>
<?php

	$message		= "Private StringFischer";

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    $encryption		= new FischerEncryption();
    
	$encryptedData	= $encryption->Encrypt($message);

    sleep(5);
    
	$decryptedData	= $encryption->Decrypt($encryptedData);

    echo "Encrypted Data: " . $encryptedData;
    echo "<br />";
    echo "5 second delay";
    
    echo "<br />";
    echo "<br />";

    echo "Decrypted Data: " . $decryptedData;

    echo "<br />";
    echo "<br />";

    $url = "http://Ron-PC/FischerExternalWeb/api/FischerExternal/Test";
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    $response = curl_exec($ch);
    echo "Call External Website: " . $response;
    curl_close($ch);
    
    echo "<br />";
    echo "<br />";

    $url = "https://Ron-PC/FischerWeb/api/FischerWebAPI/Login?user=Fischer&password=Homes";
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
    $response = curl_exec($ch);
	$key = json_decode($response);
    echo ".Net Login Key: " . $key;
    $decryptedData = $encryption->Decrypt($key);
    
    curl_close($ch);

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    sleep(8);
    
	$decryptedData	= $encryption->Decrypt($encryptedData);

    echo "<br />";
    echo "<br />";
    echo "8 more second delay";
    
    echo "<br />";

    echo "Decrypted Data: " . $decryptedData;
?>