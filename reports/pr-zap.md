# ZAP PR Scan Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 2 |
| Low | 6 |
| Informational | 3 |




## Insights

| Level | Reason | Site | Description | Statistic |
| --- | --- | --- | --- | --- |
| Low | Warning |  | ZAP errors logged - see the zap.log file for details | 50    |
| Info | Informational | http://localhost:5132 | Percentage of responses with status code 2xx | 11 % |
| Info | Informational | http://localhost:5132 | Percentage of responses with status code 4xx | 85 % |
| Info | Informational | http://localhost:5132 | Percentage of responses with status code 5xx | 2 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with content type application/json | 17 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with content type application/problem+json | 5 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with content type text/html | 11 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with method DELETE | 5 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with method GET | 76 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with method POST | 11 % |
| Info | Informational | http://localhost:5132 | Percentage of endpoints with method PUT | 5 % |
| Info | Informational | http://localhost:5132 | Count of total endpoints | 17    |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Content Security Policy (CSP) Header Not Set | Medium | 3 |
| Missing Anti-clickjacking Header | Medium | 3 |
| Application Error Disclosure | Low | 1 |
| Information Disclosure - Debug Error Messages | Low | 1 |
| Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) | Low | Systemic |
| Server Leaks Version Information via "Server" HTTP Response Header Field | Low | Systemic |
| X-AspNet-Version Response Header | Low | Systemic |
| X-Content-Type-Options Header Missing | Low | Systemic |
| Authentication Request Identified | Informational | 1 |
| Information Disclosure - Sensitive Information in URL | Informational | 1 |
| Modern Web Application | Informational | 2 |




## Alert Detail



### [ Content Security Policy (CSP) Header Not Set ](https://www.zaproxy.org/docs/alerts/10038/)



##### Medium (High)

### Description

Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks, including Cross Site Scripting (XSS) and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware. CSP provides a set of standard HTTP headers that allow website owners to declare approved sources of content that browsers should be allowed to load on that page — covered types are JavaScript, CSS, HTML frames, fonts, images and embeddable objects such as Java applets, ActiveX, audio and video files.

* URL: http://localhost:5132
  * Node Name: `http://localhost:5132`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: http://localhost:5132/
  * Node Name: `http://localhost:5132/`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: http://localhost:5132/search%3Fq=q
  * Node Name: `http://localhost:5132/search (q)`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``


Instances: 3

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Content-Security-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/CSP ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/CSP)
* [ https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html)
* [ https://www.w3.org/TR/CSP/ ](https://www.w3.org/TR/CSP/)
* [ https://w3c.github.io/webappsec-csp/ ](https://w3c.github.io/webappsec-csp/)
* [ https://web.dev/articles/csp ](https://web.dev/articles/csp)
* [ https://caniuse.com/#feat=contentsecuritypolicy ](https://caniuse.com/#feat=contentsecuritypolicy)
* [ https://content-security-policy.com/ ](https://content-security-policy.com/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Missing Anti-clickjacking Header ](https://www.zaproxy.org/docs/alerts/10020/)



##### Medium (Medium)

### Description

The response does not protect against 'ClickJacking' attacks. It should include either Content-Security-Policy with 'frame-ancestors' directive or X-Frame-Options.

* URL: http://localhost:5132
  * Node Name: `http://localhost:5132`
  * Method: `GET`
  * Parameter: `x-frame-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: http://localhost:5132/
  * Node Name: `http://localhost:5132/`
  * Method: `GET`
  * Parameter: `x-frame-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: http://localhost:5132/search%3Fq=q
  * Node Name: `http://localhost:5132/search (q)`
  * Method: `GET`
  * Parameter: `x-frame-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``


Instances: 3

### Solution

Modern Web browsers support the Content-Security-Policy and X-Frame-Options HTTP headers. Ensure one of them is set on all web pages returned by your site/app.
If you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. Alternatively consider implementing Content Security Policy's "frame-ancestors" directive.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/X-Frame-Options ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/X-Frame-Options)


#### CWE Id: [ 1021 ](https://cwe.mitre.org/data/definitions/1021.html)


#### WASC Id: 15

#### Source ID: 3

### [ Application Error Disclosure ](https://www.zaproxy.org/docs/alerts/90022/)



##### Low (Medium)

### Description

This page contains an error/warning message that may disclose sensitive information like the location of the file that produced the unhandled exception. This information can be used to launch further attacks against the web application. The alert could be a false positive if the error message is found inside a documentation page.

* URL: http://localhost:5132/debug/error
  * Node Name: `http://localhost:5132/debug/error`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 500 Internal Server Error`
  * Other Info: ``


Instances: 1

### Solution

Review the source code of this page. Implement custom error pages. Consider implementing a mechanism to provide a unique error reference/identifier to the client (browser) while logging the details on the server side and not exposing them to the user.

### Reference



#### CWE Id: [ 550 ](https://cwe.mitre.org/data/definitions/550.html)


#### WASC Id: 13

#### Source ID: 3

### [ Information Disclosure - Debug Error Messages ](https://www.zaproxy.org/docs/alerts/10023/)



##### Low (Medium)

### Description

The response appeared to contain common error messages returned by platforms such as ASP.NET, and Web-servers such as IIS and Apache. You can configure the list of common debug messages.

* URL: http://localhost:5132/debug/error
  * Node Name: `http://localhost:5132/debug/error`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Internal Server Error`
  * Other Info: ``


Instances: 1

### Solution

Disable debugging messages before pushing to production.

### Reference



#### CWE Id: [ 1295 ](https://cwe.mitre.org/data/definitions/1295.html)


#### WASC Id: 13

#### Source ID: 3

### [ Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) ](https://www.zaproxy.org/docs/alerts/10037/)



##### Low (Medium)

### Description

The web/application server is leaking information via one or more "X-Powered-By" HTTP response headers. Access to such information may facilitate attackers identifying other frameworks/components your web application is reliant upon and the vulnerabilities such components may be subject to.

* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10`
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `X-Powered-By: ASP.NET`
  * Other Info: ``
* URL: http://localhost:5132/debug/error
  * Node Name: `http://localhost:5132/debug/error`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `X-Powered-By: ASP.NET`
  * Other Info: ``
* URL: http://localhost:5132/search%3Fq=q
  * Node Name: `http://localhost:5132/search (q)`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `X-Powered-By: ASP.NET`
  * Other Info: ``
* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `X-Powered-By: ASP.NET`
  * Other Info: ``
* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10 ()({title,isComplete})`
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `X-Powered-By: ASP.NET`
  * Other Info: ``

Instances: Systemic


### Solution

Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.

### Reference


* [ https://owasp.org/www-project-web-security-testing-guide/v42/4-Web_Application_Security_Testing/01-Information_Gathering/08-Fingerprint_Web_Application_Framework ](https://owasp.org/www-project-web-security-testing-guide/v42/4-Web_Application_Security_Testing/01-Information_Gathering/08-Fingerprint_Web_Application_Framework)
* [ https://www.troyhunt.com/shhh-dont-let-your-response-headers/ ](https://www.troyhunt.com/shhh-dont-let-your-response-headers/)


#### CWE Id: [ 497 ](https://cwe.mitre.org/data/definitions/497.html)


#### WASC Id: 13

#### Source ID: 3

### [ Server Leaks Version Information via "Server" HTTP Response Header Field ](https://www.zaproxy.org/docs/alerts/10036/)



##### Low (High)

### Description

The web/application server is leaking version information via the "Server" HTTP response header. Access to such information may facilitate attackers identifying other vulnerabilities your web/application server is subject to.

* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10`
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Microsoft-IIS/10.0`
  * Other Info: ``
* URL: http://localhost:5132/debug/error
  * Node Name: `http://localhost:5132/debug/error`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Microsoft-IIS/10.0`
  * Other Info: ``
* URL: http://localhost:5132/todos
  * Node Name: `http://localhost:5132/todos ()({title,isComplete})`
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Microsoft-IIS/10.0`
  * Other Info: ``
* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10 ()({title,isComplete})`
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Microsoft-IIS/10.0`
  * Other Info: ``

Instances: Systemic


### Solution

Ensure that your web server, application server, load balancer, etc. is configured to suppress the "Server" header or provide generic details.

### Reference


* [ https://httpd.apache.org/docs/current/mod/core.html#servertokens ](https://httpd.apache.org/docs/current/mod/core.html#servertokens)
* [ https://learn.microsoft.com/en-us/previous-versions/msp-n-p/ff648552(v=pandp.10) ](https://learn.microsoft.com/en-us/previous-versions/msp-n-p/ff648552(v=pandp.10))
* [ https://www.troyhunt.com/shhh-dont-let-your-response-headers/ ](https://www.troyhunt.com/shhh-dont-let-your-response-headers/)


#### CWE Id: [ 497 ](https://cwe.mitre.org/data/definitions/497.html)


#### WASC Id: 13

#### Source ID: 3

### [ X-AspNet-Version Response Header ](https://www.zaproxy.org/docs/alerts/10061/)



##### Low (High)

### Description

Server leaks information via "X-AspNet-Version"/"X-AspNetMvc-Version" HTTP response header field(s).

* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10`
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `4.0.30319`
  * Other Info: `An attacker can use this information to exploit known vulnerabilities.`
* URL: http://localhost:5132/openapi/v1.json
  * Node Name: `http://localhost:5132/openapi/v1.json`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `4.0.30319`
  * Other Info: `An attacker can use this information to exploit known vulnerabilities.`
* URL: http://localhost:5132/search%3Fq=q
  * Node Name: `http://localhost:5132/search (q)`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `4.0.30319`
  * Other Info: `An attacker can use this information to exploit known vulnerabilities.`
* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `4.0.30319`
  * Other Info: `An attacker can use this information to exploit known vulnerabilities.`
* URL: http://localhost:5132/todos/10
  * Node Name: `http://localhost:5132/todos/10 ()({title,isComplete})`
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `4.0.30319`
  * Other Info: `An attacker can use this information to exploit known vulnerabilities.`

Instances: Systemic


### Solution

Configure the server so it will not return those headers.

### Reference


* [ https://www.troyhunt.com/shhh-dont-let-your-response-headers/ ](https://www.troyhunt.com/shhh-dont-let-your-response-headers/)
* [ https://learn.microsoft.com/en-us/archive/blogs/varunm/remove-unwanted-http-response-headers ](https://learn.microsoft.com/en-us/archive/blogs/varunm/remove-unwanted-http-response-headers)


#### CWE Id: [ 933 ](https://cwe.mitre.org/data/definitions/933.html)


#### WASC Id: 14

#### Source ID: 3

### [ X-Content-Type-Options Header Missing ](https://www.zaproxy.org/docs/alerts/10021/)



##### Low (Medium)

### Description

The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.

* URL: http://localhost:5132
  * Node Name: `http://localhost:5132`
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: http://localhost:5132/openapi/v1.json
  * Node Name: `http://localhost:5132/openapi/v1.json`
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: http://localhost:5132/search%3Fq=q
  * Node Name: `http://localhost:5132/search (q)`
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: http://localhost:5132/todos
  * Node Name: `http://localhost:5132/todos`
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: http://localhost:5132/todos
  * Node Name: `http://localhost:5132/todos ()({title,isComplete})`
  * Method: `POST`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`

Instances: Systemic


### Solution

Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.
If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.

### Reference


* [ https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85) ](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85))
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Authentication Request Identified ](https://www.zaproxy.org/docs/alerts/10111/)



##### Informational (High)

### Description

The given request has been identified as an authentication request. The 'Other Info' field contains a set of key=value lines which identify any relevant fields. If the request is in a context which has an Authentication Method set to "Auto-Detect" then this rule will change the authentication to match the request identified.

* URL: http://localhost:5132/login
  * Node Name: `http://localhost:5132/login ()({username,password})`
  * Method: `POST`
  * Parameter: `username`
  * Attack: ``
  * Evidence: `password`
  * Other Info: `userParam=username
userValue=John Doe
passwordParam=password`


Instances: 1

### Solution

This is an informational alert rather than a vulnerability and so there is nothing to fix.

### Reference


* [ https://www.zaproxy.org/docs/desktop/addons/authentication-helper/auth-req-id/ ](https://www.zaproxy.org/docs/desktop/addons/authentication-helper/auth-req-id/)



#### Source ID: 3

### [ Information Disclosure - Sensitive Information in URL ](https://www.zaproxy.org/docs/alerts/10024/)



##### Informational (Medium)

### Description

The request appeared to contain sensitive information leaked in the URL. This can violate PCI and most organizational compliance policies. You can configure the list of strings for this check to add or remove values specific to your environment.

* URL: http://localhost:5132/export%3Ftoken=token
  * Node Name: `http://localhost:5132/export (token)`
  * Method: `GET`
  * Parameter: `token`
  * Attack: ``
  * Evidence: `token`
  * Other Info: `The URL contains potentially sensitive information. The following string was found via the pattern: token
token`


Instances: 1

### Solution

Do not pass sensitive information in URIs.

### Reference



#### CWE Id: [ 598 ](https://cwe.mitre.org/data/definitions/598.html)


#### WASC Id: 13

#### Source ID: 3

### [ Modern Web Application ](https://www.zaproxy.org/docs/alerts/10109/)



##### Informational (Medium)

### Description

The application appears to be a modern web application. If you need to explore it automatically then the Ajax Spider may well be more effective than the standard one.

* URL: http://localhost:5132
  * Node Name: `http://localhost:5132`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script>
    const api = '';

    async function loadTodos() {
      const res = await fetch(api + '/todos');
      const todos = await res.json();
      const list = document.getElementById('todo-list');
      list.innerHTML = '';
      for (const t of todos) {
        const li = document.createElement('li');
        if (t.isComplete) li.classList.add('done');
        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.checked = t.isComplete;
        checkbox.onchange = () => toggleTodo(t);
        const span = document.createElement('span');
        span.textContent = t.title;
        span.style.flex = '1';
        const del = document.createElement('button');
        del.className = 'delete';
        del.textContent = 'X';
        del.onclick = () => deleteTodo(t.id);
        li.append(checkbox, span, del);
        list.appendChild(li);
      }
    }

    async function addTodo() {
      const input = document.getElementById('new-title');
      const title = input.value.trim();
      if (!title) return;
      await fetch(api + '/todos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title, isComplete: false })
      });
      input.value = '';
      loadTodos();
    }

    async function toggleTodo(t) {
      await fetch(api + '/todos/' + t.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: t.title, isComplete: !t.isComplete })
      });
      loadTodos();
    }

    async function deleteTodo(id) {
      await fetch(api + '/todos/' + id, { method: 'DELETE' });
      loadTodos();
    }

    async function doSearch() {
      const q = document.getElementById('search-q').value;
      const res = await fetch(api + '/search?q=' + encodeURIComponent(q));
      document.getElementById('search-result').innerHTML = await res.text();
    }

    async function doLogin() {
      const username = document.getElementById('username').value;
      const password = document.getElementById('password').value;
      const res = await fetch(api + '/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
      });
      document.getElementById('login-result').textContent =
        res.ok ? 'Logged in' : 'Invalid credentials';
    }

    document.getElementById('add-btn').onclick = addTodo;
    document.getElementById('search-btn').onclick = doSearch;
    document.getElementById('login-btn').onclick = doLogin;
    document.getElementById('new-title').addEventListener('keydown', e => {
      if (e.key === 'Enter') addTodo();
    });

    loadTodos();
  </script>`
  * Other Info: `No links have been found while there are scripts, which is an indication that this is a modern web application.`
* URL: http://localhost:5132/
  * Node Name: `http://localhost:5132/`
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script>
    const api = '';

    async function loadTodos() {
      const res = await fetch(api + '/todos');
      const todos = await res.json();
      const list = document.getElementById('todo-list');
      list.innerHTML = '';
      for (const t of todos) {
        const li = document.createElement('li');
        if (t.isComplete) li.classList.add('done');
        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.checked = t.isComplete;
        checkbox.onchange = () => toggleTodo(t);
        const span = document.createElement('span');
        span.textContent = t.title;
        span.style.flex = '1';
        const del = document.createElement('button');
        del.className = 'delete';
        del.textContent = 'X';
        del.onclick = () => deleteTodo(t.id);
        li.append(checkbox, span, del);
        list.appendChild(li);
      }
    }

    async function addTodo() {
      const input = document.getElementById('new-title');
      const title = input.value.trim();
      if (!title) return;
      await fetch(api + '/todos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title, isComplete: false })
      });
      input.value = '';
      loadTodos();
    }

    async function toggleTodo(t) {
      await fetch(api + '/todos/' + t.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: t.title, isComplete: !t.isComplete })
      });
      loadTodos();
    }

    async function deleteTodo(id) {
      await fetch(api + '/todos/' + id, { method: 'DELETE' });
      loadTodos();
    }

    async function doSearch() {
      const q = document.getElementById('search-q').value;
      const res = await fetch(api + '/search?q=' + encodeURIComponent(q));
      document.getElementById('search-result').innerHTML = await res.text();
    }

    async function doLogin() {
      const username = document.getElementById('username').value;
      const password = document.getElementById('password').value;
      const res = await fetch(api + '/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
      });
      document.getElementById('login-result').textContent =
        res.ok ? 'Logged in' : 'Invalid credentials';
    }

    document.getElementById('add-btn').onclick = addTodo;
    document.getElementById('search-btn').onclick = doSearch;
    document.getElementById('login-btn').onclick = doLogin;
    document.getElementById('new-title').addEventListener('keydown', e => {
      if (e.key === 'Enter') addTodo();
    });

    loadTodos();
  </script>`
  * Other Info: `No links have been found while there are scripts, which is an indication that this is a modern web application.`


Instances: 2

### Solution

This is an informational alert and so no changes are required.

### Reference




#### Source ID: 3


