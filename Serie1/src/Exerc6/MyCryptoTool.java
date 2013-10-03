package Exerc6;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;

public class MyCryptoTool {
	private byte[] hashGood ;
	private byte[] hashBad ;
	
	public MyCryptoTool() throws NoSuchAlgorithmException, IOException{
		hashGood = produceHash("c:\\GoodApp.java");
		hashBad = produceHash("c:\\BadApp.java");
		
	}
	
	private byte[] produceHash(String filePath) throws NoSuchAlgorithmException, IOException {
		MessageDigest md =  MessageDigest.getInstance("SHA1");
		InputStream is = new FileInputStream(filePath);
		
		byte[] data = new byte[4096];
		
		byte b; int i= 0;
		
		while ((b=(byte) is.read())!=-1 && i < data.length) {
			data[i] = b ;
			i++;
		}
		is.close();
		return md.digest(Arrays.copyOf(data, i));
	}
	
	private byte[] readGood(int nBytes){
		//return hashGood;
		return Arrays.copyOf(hashGood, nBytes);
		
	}
	
	private byte[] readBad(int nBytes){
		
		//return hashBad;
		return Arrays.copyOf(hashBad, nBytes);
			
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		
		MyCryptoTool crypto = new MyCryptoTool();
		crypto.Question51(1);
		crypto.Question51(2);
		crypto.Question51(4);
		
		
	}
	
	private void Question51( int nBytes) 
	{
		System.out.print("GoodApp Hash: ");
		for (byte b : this.readGood(nBytes))
		{
			System.out.printf("%x", b);
		}
		System.out.print("\nBadApp Hash: ");
		for (byte b : this.readBad(nBytes))
		{
			System.out.printf("%x", b);
	
		}
		System.out.println();
	}
}
