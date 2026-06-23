/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CalculationRequest } from '../models/CalculationRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CalculatorService {

    /**
     * @returns any OK
     * @throws ApiError
     */
    public static postApiCalculatorNewSession(): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Calculator/new-session',
        });
    }

    /**
     * @param requestBody 
     * @returns any OK
     * @throws ApiError
     */
    public static postApiCalculatorCalculate(
requestBody?: CalculationRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Calculator/calculate',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @param page 
     * @param pageSize 
     * @returns any OK
     * @throws ApiError
     */
    public static getApiCalculatorHistory(
page: number = 1,
pageSize: number = 2,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/Calculator/history',
            query: {
                'page': page,
                'pageSize': pageSize,
            },
        });
    }

}
