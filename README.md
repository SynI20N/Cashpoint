<h1>Cashpoint imitation using dynamic programming algorithm</h1>
<h2>Prerequisites: ASP.NET Core 8.0</h2>
<p>Compile:<code>dotnet build</code></p>
<p>Run:<code>dotnet run</code></p>
<p>Run with docker: <code>sudo docker compose up -d</code></p>

<p>How different choices affect algorithm:</p>
<table>
  <tr>
    <td><img src="https://github.com/SynI20N/Cashpoint/blob/main/img/1.png" height="400" width="400"></td>
    <td><img src="https://github.com/SynI20N/Cashpoint/blob/main/img/2.png" height="400" width="400"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/SynI20N/Cashpoint/blob/main/img/3.png" height="400" width="400"></td>
    <td><img src="https://github.com/SynI20N/Cashpoint/blob/main/img/4.png" height="400" width="400"></td>
  </tr>
</table>

<p>Tough case with prime numbers:</p>
<table>
  <tr>
    <td><img src="https://github.com/SynI20N/Cashpoint/blob/main/img/5.png" height="400" width="400"></td>
  </tr>
</table>
<h2>DDOS attack protection</h2>
<p>Leaky bucket is used to protect from DDOS as a middleware</p>
<p>For the following reasons:
  <ul>
  <li>Easy to implement</li>
  <li>Faster than other algorithms</li>
  </ul>
</p>
<h2>Load testing</h2>
<p>Artillery-image container is built from Dockerfile_test</p>
<p>This container runs artillery to load test on Cashpoint specific port</p>
<p>The result.json contains info from this load-testing</p>
