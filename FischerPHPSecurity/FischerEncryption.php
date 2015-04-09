<?php

	class FischerEncryption {
    
		private	$encryptionKey	= "passcodefischerhomes1234";	// 24 bytes for 3DES/ECB encryption

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public function Encrypt($message) {
            
			$td = mcrypt_module_open('tripledes', '', 'ecb', '');
			$iv = mcrypt_create_iv(mcrypt_enc_get_iv_size($td), MCRYPT_RAND);
			mcrypt_generic_init($td, $this->encryptionKey, $iv);

            // Add Timestamp
            $dateTime = new DateTime("now");
            $message = $dateTime->format("YmdHis") . $message;
            
			// PKCS7 Padding
			$b = mcrypt_get_block_size('tripledes', 'ecb');
			$dataPad = $b-(strlen($message)%$b);
			$message .= str_repeat(chr($dataPad), $dataPad);

			$encrypted_data = bin2hex(mcrypt_generic($td, $message));
			mcrypt_generic_deinit($td);
			mcrypt_module_close($td);

			return $encrypted_data;
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public function Decrypt($message) {

			$td = mcrypt_module_open('tripledes', '', 'ecb', '');
			$iv = mcrypt_create_iv(mcrypt_enc_get_iv_size($td), MCRYPT_RAND);
			mcrypt_generic_init($td, $this->encryptionKey, $iv);

			$decrypted_data = mdecrypt_generic($td, hex2bin($message));
			mcrypt_generic_deinit($td);
			mcrypt_module_close($td);

			// PKCS7 Padding
			$dataPad		= ord($decrypted_data[strlen($decrypted_data)-1]);
			$decrypted_data = substr($decrypted_data, 0, -$dataPad);

			// Validate Key Timeout
            try
            {
				if(strlen($decrypted_data) < 14)
					return null;
				$timestampStr	= substr($decrypted_data, 0, 14);
				$decrypted_data = substr($decrypted_data, 14);
				$datetimeNow	= new DateTime("now");
				$timestamp		= new DateTime("now");
				$timestamp		= $timestamp->setDate(substr($timestampStr, 0, 4), substr($timestampStr, 4, 2), substr($timestampStr, 6, 2));
				$timestamp		= $timestamp->setTime(substr($timestampStr, 8, 2), substr($timestampStr, 10, 2), substr($timestampStr, 12, 2));
				$elapseTime		= $datetimeNow->diff($timestamp);
                if($elapseTime->s > 10)
                    return null;
            }
            catch(Exception $ex)
            {
				return null;
			}
            return $decrypted_data;
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

	}

?>