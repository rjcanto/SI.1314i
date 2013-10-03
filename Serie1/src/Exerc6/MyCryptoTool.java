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
	
	private byte[] read(int nBits, byte[] h) {
		int nFullBytes = nBits / 8;
		byte[] result ;
		
		result = (nBits%8 > 0)?new byte[nFullBytes +1]:new byte[nFullBytes];
				
		if (nBits>=8) {
			System.arraycopy(h, 0, result, 0, nFullBytes);
			int remainingBits = nBits - (nFullBytes)*8;
			if(remainingBits==0)
				return result;
		
			result[nFullBytes+1] = (byte) (h[nFullBytes] >> (8-remainingBits));
			return result;
		}
		
		result[0] = (byte) (h[0] >> (nBits));
		return result ;
		
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		
		MyCryptoTool crypto = new MyCryptoTool();
		crypto.Question51(8);
		crypto.Question51(16);
		crypto.Question51(32);
		
	}
	
	private void Question51( int nBits) 
	{
		System.out.print("GoodApp Hash: ");
		for (byte b : this.read(nBits,hashGood))
		{
			System.out.printf("%x", b);
		}
		System.out.print("\nBadApp Hash: ");
		for (byte b : this.read(nBits,hashBad))
		{
			System.out.printf("%x", b);
	
		}
		System.out.println();
	}
}
