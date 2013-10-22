package Exerc6;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.AlgorithmParameters;
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
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;

import com.sun.xml.internal.messaging.saaj.util.Base64;

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
		//SecretKeyFactory f = SecretKeyFactory.getInstance("AES");
		Random r = new SecureRandom();
		r.nextBytes(options.salt); 
		//byte[] key = new byte[20];
		/*
		
		KeySpec spec = new PBEKeySpec(password.toCharArray(),
										options.salt,
										options.iterations,
										options.saltBits
									);
		SecretKey k =f.generateSecret(spec);
		return new SecretKeySpec(k.getEncoded(),"AES");
		*/
		MessageDigest digester = MessageDigest.getInstance("SHA-256");
		digester.update(password.getBytes("UTF-8"));
		byte[] key = digester.digest();
		//System.out.println(key.length);
		return new SecretKeySpec(key,"AES");
	}
	
	public byte[] encrypt(SecretKey k, IronOptions options, String data) throws NoSuchAlgorithmException, NoSuchPaddingException, IllegalBlockSizeException, BadPaddingException, IOException, InvalidKeyException, InvalidParameterSpecException, InvalidAlgorithmParameterException {
		Random r = new SecureRandom();
		byte[] hmacSalt = new byte[10] ;
		byte[] hmac = new byte[10] ;
		ByteArrayOutputStream sealedObjStream = new ByteArrayOutputStream( );
		
		String macPrefix = "fe26.2" ;
		
		r.nextBytes(hmacSalt);
		

		 AlgorithmParameters algParams;
	    algParams = AlgorithmParameters.getInstance("AES");


		
		Cipher cipher = Cipher.getInstance("AES");
		cipher.init(Cipher.ENCRYPT_MODE, k,algParams);
		//AlgorithmParameters params = cipher.getParameters();
		//byte[] iv = params.getParameterSpec(IvParameterSpec.class).getIV();
		byte[] iv = algParams.getParameterSpec(IvParameterSpec.class).getIV();
		byte[] ciphertext = cipher.doFinal(data.getBytes());
		byte[] encryptedB64 = Base64.encode(ciphertext);
		
		sealedObjStream.write(macPrefix.getBytes());
		sealedObjStream.write("**".getBytes());
		sealedObjStream.write(options.salt);
		sealedObjStream.write("*".getBytes());
		sealedObjStream.write(iv);
		sealedObjStream.write("*".getBytes());
		sealedObjStream.write(encryptedB64);
		sealedObjStream.write("**".getBytes());
		
		Random hmacRandom = new SecureRandom(sealedObjStream.toByteArray( ));
		hmacRandom.nextBytes(hmac) ;
		
		sealedObjStream.write(hmacSalt);
		sealedObjStream.write("*".getBytes());
		sealedObjStream.write(hmac);
		
		return sealedObjStream.toByteArray( );
	}
	
	
	public String decrypt(SecretKey k, IronOptions options) {
		return null;
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, InvalidKeySpecException, NoSuchPaddingException, InvalidKeyException, InvalidParameterSpecException, IllegalBlockSizeException, BadPaddingException, IOException, InvalidAlgorithmParameterException {
		final String password = "TESTE";
		final String jsonObj = "{\"course\":\"LEIC\",\"is_Optional\":true,\"name\":\"SI1314i\"}" ;
		
		MyJavaIron iron = new MyJavaIron() ;
		IronOptions io = new IronOptions();
		 
		System.out.println(iron.encrypt(iron.generate(password,io), io, jsonObj).toString());
	}
}
