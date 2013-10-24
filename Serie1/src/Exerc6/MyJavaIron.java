package Exerc6;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.InvalidParameterSpecException;
import java.security.spec.KeySpec;
import java.util.Random;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;

import sun.misc.BASE64Encoder;

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
		
		String macPrefix = "fe26.2" ;
		
		r.nextBytes(hmacSalt);

		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
		cipher.init(Cipher.ENCRYPT_MODE, k, new SecureRandom());
		byte[] iv = cipher.getIV();
		
		byte[] ciphertext = cipher.doFinal(data.getBytes());
		
		BASE64Encoder encoder = new BASE64Encoder();
		
		String sealed =
				macPrefix + "**" +
				encoder.encode(options.salt) + "*" +
				encoder.encode(iv) + "*" +		
				encoder.encode(ciphertext) + "**";
		
		Random hmacRandom = new SecureRandom(sealed.getBytes());
		hmacRandom.nextBytes(hmac) ;
		
		return sealed + encoder.encode(hmacSalt) + "*" + encoder.encode(hmac);
	}
	
	
	public String decrypt(SecretKey k, IronOptions options) {
		return null;
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, InvalidKeySpecException, NoSuchPaddingException, InvalidKeyException, InvalidParameterSpecException, IllegalBlockSizeException, BadPaddingException, IOException, InvalidAlgorithmParameterException {
		final String password = "TESTE";
		//final String jsonObj = "{\"course\":\"LEIC\",\"is_Optional\":true,\"name\":\"SI1314i\"}" ;
		final String jsonObj = "{a: 6}" ;
		
		MyJavaIron iron = new MyJavaIron() ;
		IronOptions io = new IronOptions();
		 
		System.out.println(iron.encrypt(iron.generate(password,io), io, jsonObj));
	}
}
