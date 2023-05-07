<h1>ChatServerClient</h1> 

<h3>Quick summary</h3>
<ul>
  <li>This chat app client is part of my chatapp project and serves as example client for chatting service</li>
  <li>Client can connect and disconnect at any time</li>
  <li>The recieval of messages is asynchrous so it doesnt disturb users experience and allows to send/receive at any time</li>
</ul>

</p>
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
