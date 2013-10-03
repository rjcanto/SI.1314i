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
	

	private byte[] read(int nBits, byte[] h) {
		int nFullBytes = nBits / 8;
		byte[] result ;
		
		result = (nBits%8 > 0)?new byte[nFullBytes +1]:new byte[nFullBytes];
				
		if (nBits>=8) {
			System.arraycopy(h, 0, result, 0, nFullBytes);
			int remainingBits = nBits - (nFullBytes)*8;
			if(remainingBits==0)
				return result;
		
			//Para shiftar é necessário converter para int e colocar a zero todos os octetos
			//excepto o primeiro
			int x = ((int) h[nFullBytes]) & 0xFF;
			result[nFullBytes] = (byte) (x >> (8-remainingBits));
			return result;
		}
		int x = ((int) h[0]) & 0xFF;
		result[0] = (byte) (x >> (nBits));
		return result ;
		
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		
		MyCryptoTool crypto = new MyCryptoTool();
		
		System.out.println("----------------");
		crypto.Question51(4);
		crypto.Question51(8);
		System.out.println("----------------");
		crypto.Question51(12);
		crypto.Question51(16);
		System.out.println("----------------");
		crypto.Question51(36);
		crypto.Question51(40);
		
		
		
		
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
