import React from 'react';
import { connect } from 'react-redux';

const Home = props => (
    <div>
        <h1>Hello, world!</h1>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
            <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
            <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
            <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <p>To help you get started, we've also set up:</p>
        <ul>
            <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
            <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
            <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>

        <div className="container">
            <i type="light" className="fal text-lg text-danger mr-1 fa-exclamation-triangle"></i><span className="disclaimer"><a className="text-danger text-xs underline" href="/pt/isen%C3%A7%C3%A3o_de_responsabilidade">ISEN��O DE RESPONSABILIDADE IMPORTANTE:</a></span>
            <span className="text-lite text-xs">Todos os conte�dos fornecidos no nosso website, sites hiperligados, aplica��es associadas, f�runs, blogs, contas nas redes sociais e noutras plataformas ("Site") s�o apenas para sua informa��o geral, obtida por fontes terceiras. N�o fazemos garantias de qualquer tipo em rela��o ao nosso conte�do, por exemplo, a precis�o e atualiza��o. Nenhuma parte do conte�do que fornecemos constitui conselhos financeiros, conselhos jur�dicos ou qualquer outra forma de conselhos para sua confian�a espec�fica por qualquer motivo. Qualquer utiliza��o ou fiabilidade sobre o nosso conte�do � unicamente da sua responsabilidade. Deve realizar a sua pr�pria pesquisa, avalia��o, an�lise e verificar o nosso conte�do antes de confiar nele. Os c�mbios s�o uma atividade de alto risco que podem levar a grandes perdas, portanto, consulte o seu consultor financeiro antes de tomar qualquer decis�o. Nenhum conte�do no nosso Site � uma solicita��o ou oferta.</span>
        </div>
    </div>
);

export default connect()(Home);
