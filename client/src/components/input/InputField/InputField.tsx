import React, {FC} from 'react';
import {Label} from 'flowbite-react';
import DefaultInput from "../DefaultInput/DefaultInput";

interface Props {
    onChange?:(e: any) => void,
    value?:string | number | boolean | Date | null | File,
    id:string,
    type:string,
    placeholder?:string,
    required:boolean,
    labelValue:string,
    min?:number,
    max?:number,
    readonly?:boolean,
    pattern?:string,
    title?:string
}

const InputField:FC<Props> = ({
                                  onChange,
                                  value,
                                  id,
                                  type,
                                  placeholder,
                                  required,
                                  labelValue,
                                  min,
                                  max,
                                  readonly,
                                  pattern,
                                  title
                              }) => {
    return (
        <div>
            <div className="mb-2 block">
                <Label
                    htmlFor={id}
                    value={labelValue}
                />
            </div>
            <DefaultInput
                onChange={onChange}
                value={value}
                id={id}
                type={type}
                placeholder={placeholder}
                required={required}
                min={min}
                max={max}
                readOnly={readonly}
                pattern={pattern}
                title={title}
            />
        </div>
    );
};

export default InputField;