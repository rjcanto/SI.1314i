package Exerc6;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.InvalidParameterSpecException;
import java.security.spec.KeySpec;
import java.util.Random;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.Mac;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;

import org.apache.commons.codec.binary.Base64;

/**
 * 
 * @author Ricardo Canto
 * @author Luís Brás
 *
 * A small library to use Iron in Java
 * https://github.com/hueniverse/iron
 */

public class MyJavaIron {
	public MyJavaIron() {
		
	}
	
	public SecretKey generate(String password, IronOptions options) throws NoSuchAlgorithmException, InvalidKeySpecException, UnsupportedEncodingException {
		SecretKeyFactory f = SecretKeyFactory.getInstance("PBKDF2WithHMacSHA1");
		Random r = new SecureRandom();
		r.nextBytes(options.salt);		
		
		KeySpec spec = new PBEKeySpec(password.toCharArray(),
										options.salt,
										options.iterations,
										256
									);
		SecretKey k = f.generateSecret(spec);
		return new SecretKeySpec(k.getEncoded(), "AES");
	}
	
	public String encrypt(SecretKey k, IronOptions options, String data) throws NoSuchAlgorithmException, NoSuchPaddingException, IllegalBlockSizeException, BadPaddingException, IOException, InvalidKeyException, InvalidParameterSpecException, InvalidAlgorithmParameterException {
		Random r = new SecureRandom();
		byte[] hmacSalt = new byte[10] ;
		byte[] hmac = new byte[10] ;		
		
		String macPrefix = "Fe26.2" ;
		
		r.nextBytes(hmacSalt);

		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
		cipher.init(Cipher.ENCRYPT_MODE, k, new SecureRandom());
		byte[] iv = cipher.getIV();
		
		byte[] ciphertext = cipher.doFinal(data.getBytes());		
		
		String sealed =
				macPrefix + "**" +
				Base64.encodeBase64URLSafeString(options.salt) + "*" +
				Base64.encodeBase64URLSafeString(iv) + "*" +		
				Base64.encodeBase64URLSafeString(ciphertext) + "*";
		
		Random hmacRandom = new SecureRandom(sealed.getBytes());
		hmacRandom.nextBytes(hmac) ;
		
		Mac m = Mac.getInstance("HmacSHA256");
		m.init(k);
		m.update(sealed.getBytes());
		
		String hmacDigest = Base64.encodeBase64URLSafeString(m.doFinal()).replaceAll("/\\+/g", "-").replaceAll("/\\//g", "_").replaceAll("/\\=/g", "");
		
		return sealed + "*" + Base64.encodeBase64URLSafeString(options.salt) + "*" + hmacDigest;
	}
	
	
	public String decrypt(SecretKey k, String sealed) throws IllegalBlockSizeException, BadPaddingException, InvalidKeyException, NoSuchAlgorithmException, NoSuchPaddingException, UnsupportedEncodingException, InvalidAlgorithmParameterException {
		IronOptions ioDecrypt = new IronOptions();
		String[] parts = sealed.split("\\*");
		
		if (parts.length != 8) {
	        throw new IllegalArgumentException();
	    }
		
		String macPrefix = parts[0];
		String passwordId = parts[1];
		String encryptionSalt = parts[2];
		String encryptionIv = parts[3];
		String encryptedB64 = parts[4];
		String expiration = parts[5];
		String hmacSalt = parts[6];
		String hmac = parts[7];
		String macBaseString = macPrefix + '*' + passwordId + '*' + encryptionSalt + '*' + encryptionIv + '*' + encryptedB64 + '*' + expiration;
		
		byte[] encrypted = Base64.decodeBase64(encryptedB64);
		byte[] iv = Base64.decodeBase64(encryptionIv); 
		ioDecrypt.salt = encryptionSalt.getBytes();
		
		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
		cipher.init(Cipher.DECRYPT_MODE, k, new IvParameterSpec(iv));
			
		return new String(cipher.doFinal(encrypted), "UTF-8");
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, InvalidKeySpecException, NoSuchPaddingException, InvalidKeyException, InvalidParameterSpecException, IllegalBlockSizeException, BadPaddingException, IOException, InvalidAlgorithmParameterException {
		final String password = "TESTE";
		//final String jsonObj = "{\"course\":\"LEIC\",\"is_Optional\":true,\"name\":\"SI1314i\"}" ;
		final String jsonObj = "{a: 6}" ;
		
		MyJavaIron iron = new MyJavaIron() ;
		IronOptions io = new IronOptions();
		SecretKey myKey = iron.generate(password,io);
		String sealed = iron.encrypt(myKey, io, jsonObj) ;
		System.out.println(sealed);
		
		System.out.println(iron.decrypt(myKey, sealed));
	}
}
