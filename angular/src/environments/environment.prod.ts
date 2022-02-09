import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Tulumba',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://localhost:44389',
    redirectUri: baseUrl,
    clientId: 'Tulumba_App',
    responseType: 'code',
    scope: 'offline_access openid profile role email phone Tulumba',
    requireHttps: false
  },
  apis: {
    default: {
      url: 'http://localhost:44389',
      rootNamespace: 'Tulumba',
    },
  },
} as Environment;
