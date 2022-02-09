import { Environment } from '@abp/ng.core';

const baseUrl = '';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Tulumba',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: '',
    redirectUri: baseUrl,
    clientId: 'Tulumba_App',
    responseType: 'code',
    scope: 'offline_access Tulumba',
    requireHttps: true,
    strictDiscoveryDocumentValidation: false, 
  },
  apis: {
    default: {
      url: '',
      rootNamespace: 'Tulumba',
    },
  },
} as Environment;
