package exerc1;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.security.cert.X509Certificate;
import java.util.List;


public class Program {

	public static void main(String[] args) {

	      System.out.print("Enter the URI to check for certificates expiring in 30 days: ");

	      BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

	      String uri = null;

	      try {
	         uri = br.readLine();
	      } catch (IOException ioe) {
	         System.out.println("IO error trying to read the URI.");
	         System.exit(1);
	      }
		
		try {
			
			CertValidator validator = new CertValidator();
			
			List<X509Certificate> expiredCerts = validator.certInChainExpiresInLessThan(30, uri, 443);
			
			if (expiredCerts.isEmpty()) {
				System.out.println("All certificates are valid for the next 30 days.");
				System.exit(0);
			}
			
			System.out.println("The following certificates will expire on the next 30 days.");
			for (X509Certificate x509Cert : expiredCerts)
			{
				System.out.println("**************************");
				System.out.println("Name: " + x509Cert.getSubjectX500Principal().getName());
				System.out.println("Expiration date: " + x509Cert.getNotAfter().toString());
			}
			
		} catch (Exception e) {
			System.out.println("Error while checking certificate chain.");
		}
		
	}

}
