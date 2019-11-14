import React, { Component } from 'react';
import PropTypes from 'prop-types';

import { StatusBar, AsyncStorage } from 'react-native';
import { StackActions, NavigationActions } from 'react-navigation';

import api from '../../services/api';

import {
    Container,
    Logo,
    Input,
    ErrorMessage,
    Button,
    ButtonText,
    SignUpLink,
    SignUpLinkText,
} from './styles';

export default class SignUp extends Component {
    static navigationOptions = {
        header: null,
    };

    static propTypes = {
        navigation: PropTypes.shape({
            navigate: PropTypes.func,
            dispatch: PropTypes.func,
        }).isRequired,
    };

    state = { email: '', password: '', error: '' };

    handleEmailChange = (email) => {
        this.setState({ email });
    };

    handlePasswordChange = (password) => {
        this.setState({ password });
    };

    handleCreateAccountPress = () => {
        this.props.navigation.navigate('SignUp');
    };

    handleSignUpPress = async () => {
        if (this.state.email.length === 0 || this.state.password.length === 0) {
            this.setState({ error: 'Preencha usuário e senha para continuar!' }, () => false);
        } else {
            try {
                const response = await api.post('account/register', {
                    nome: this.state.nome,
                    sobrenome: this.state.sobrenome,
                    email: this.state.email,
                    password: this.state.password,
                    confirmPassword: this.state.confirmPassword
                });
                const resetAction = StackActions.reset({
                    index: 0,
                    actions: [
                        NavigationActions.navigate({ routeName: 'Main' }),
                    ],
                });
                this.props.navigation.dispatch(resetAction);
            } catch (_err) {
                this.setState({ error: 'Houve um problema com o login, verifique suas credenciais!' });
            }
        }
    };

    render() {
        return (
            <Container>
                <StatusBar hidden />
                
                <Input
                    placeholder="Nome"
                    value={this.state.nome}
                    onChangeText={this.handleEmailChange}
                    autoCapitalize="none"
                    autoCorrect={false}
                />
                <Input
                    placeholder="Sobrenome"
                    value={this.state.sobrenome}
                    onChangeText={this.handleEmailChange}
                    autoCapitalize="none"
                    autoCorrect={false}
                />
                <Input
                    placeholder="Endereço de e-mail"
                    value={this.state.email}
                    onChangeText={this.handleEmailChange}
                    autoCapitalize="none"
                    autoCorrect={false}
                />
                <Input
                    placeholder="Senha"
                    value={this.state.password}
                    onChangeText={this.handlePasswordChange}
                    autoCapitalize="none"
                    autoCorrect={false}
                    secureTextEntry
                />
                <Input
                    placeholder="Confirmar Senha"
                    value={this.state.confirmPassword}
                    onChangeText={this.handlePasswordChange}
                    autoCapitalize="none"
                    autoCorrect={false}
                    secureTextEntry
                />
                {this.state.error.length !== 0 && <ErrorMessage>{this.state.error}</ErrorMessage>}
                <Button onPress={this.handleSignUpPress}>
                    <ButtonText>Cadastrar</ButtonText>
                </Button>
            </Container>
        );
    }
}