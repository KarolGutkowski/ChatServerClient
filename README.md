<h1>ChatServerClient</h1> 

<p>This is an example client for my ChatAppServer.</br>
App allows users to communicate by messages through the server.</br>
The process of receiveing messages is asynchronous so that client isn't interrupted by incoming messages or doesnt have to put messages in ceratin order.

How to test it for yourself ?
<ul>
  <li>Create MS SQL database called ChatAppDB or change the connection string in App.config of server
    (<i>note that this is written to work with MS SQL connection</i>)
    <br/>
    (server code repo: https://github.com/KarolGutkowski/ChatAppServer)</li>
  <li>in the database create Users with columns: user_id, login, password (those are mandatory)</li>
  <li>Run the server app from the repo listed above</li>
  <li>Run the client app</li>
  </ul>
