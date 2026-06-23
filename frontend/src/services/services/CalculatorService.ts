/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Calculation } from '../models/Calculation';
import type { CalculationRequest } from '../models/CalculationRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CalculatorService {

    /**
     * @returns Calculation OK
     * @throws ApiError
     */
    public static getApiCalculatorHistory(): CancelablePromise<Array<Calculation>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/Calculator/History',
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

}
