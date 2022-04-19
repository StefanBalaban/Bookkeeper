FROM node:14.18.1 as build

WORKDIR /app

COPY package.json replace_env-variables.sh ./
ARG ANGULAR_CLIENT_URL_BASE
ARG API_HOST_URL_BASE
ENV ANGULAR_CLIENT_URL_BASE "$ANGULAR_CLIENT_URL_BASE"
ENV API_HOST_URL_BASE "$API_HOST_URL_BASE"


RUN yarn install
COPY . .
RUN yarn build --prod


### STAGE 2: Run ###
FROM nginx as final

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/Tulumba /usr/share/nginx/html
COPY --from=build /app/replace_env-variables.sh /

CMD ["sh", "replace_env-variables.sh"]

# Expose port 80
EXPOSE 80
