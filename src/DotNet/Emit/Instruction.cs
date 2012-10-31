﻿using System;
using System.Collections.Generic;

namespace dot10.DotNet.Emit {
	/// <summary>
	/// A CIL instruction (opcode + operand)
	/// </summary>
	public sealed class Instruction {
		/// <summary>
		/// The opcode
		/// </summary>
		public OpCode OpCode;

		/// <summary>
		/// The opcode operand
		/// </summary>
		public object Operand;

		/// <summary>
		/// Offset of the instruction in the method body
		/// </summary>
		public uint Offset;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Instruction() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="opCode">Opcode</param>
		public Instruction(OpCode opCode) {
			this.OpCode = opCode;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="opCode">Opcode</param>
		/// <param name="operand">The operand</param>
		public Instruction(OpCode opCode, object operand) {
			this.OpCode = opCode;
			this.Operand = operand;
		}

		/// <summary>
		/// Creates a new instruction with no operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode) {
			if (opCode.OperandType != OperandType.InlineNone)
				throw new ArgumentException("Must be a no-operand opcode", "opCode");
			return new Instruction(opCode);
		}

		/// <summary>
		/// Creates a new instruction with a <see cref="byte"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, byte value) {
			if (opCode.Code != Code.Unaligned)
				throw new ArgumentException("Opcode does not have a byte operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with a <see cref="sbyte"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, sbyte value) {
			if (opCode.Code != Code.Ldc_I4_S)
				throw new ArgumentException("Opcode does not have a sbyte operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with an <see cref="int"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, int value) {
			if (opCode.OperandType != OperandType.InlineI)
				throw new ArgumentException("Opcode does not have an int32 operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with a <see cref="long"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, long value) {
			if (opCode.OperandType != OperandType.InlineI8)
				throw new ArgumentException("Opcode does not have an int64 operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with a <see cref="float"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, float value) {
			if (opCode.OperandType != OperandType.ShortInlineR)
				throw new ArgumentException("Opcode does not have a real4 operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with a <see cref="double"/> operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="value">The value</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, double value) {
			if (opCode.OperandType != OperandType.InlineR)
				throw new ArgumentException("Opcode does not have a real8 operand", "opCode");
			return new Instruction(opCode, value);
		}

		/// <summary>
		/// Creates a new instruction with a string operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="s">The string</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, string s) {
			if (opCode.OperandType != OperandType.InlineString)
				throw new ArgumentException("Opcode does not have a string operand", "opCode");
			return new Instruction(opCode, s);
		}

		/// <summary>
		/// Creates a new instruction with an instruction target operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="target">Target instruction</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, Instruction target) {
			if (opCode.OperandType != OperandType.ShortInlineBrTarget && opCode.OperandType != OperandType.InlineBrTarget)
				throw new ArgumentException("Opcode does not have an instruction operand", "opCode");
			return new Instruction(opCode, target);
		}

		/// <summary>
		/// Creates a new instruction with an instruction target list operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="targets">The targets</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, IList<Instruction> targets) {
			if (opCode.OperandType != OperandType.InlineSwitch)
				throw new ArgumentException("Opcode does not have a targets array operand", "opCode");
			return new Instruction(opCode, targets);
		}

		/// <summary>
		/// Creates a new instruction with a type operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="type">The type</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, ITypeDefOrRef type) {
			if (opCode.OperandType != OperandType.InlineType)
				throw new ArgumentException("Opcode does not have a type operand", "opCode");
			return new Instruction(opCode, type);
		}

		/// <summary>
		/// Creates a new instruction with a field operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="field">The field</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, IField field) {
			if (opCode.OperandType != OperandType.InlineField)
				throw new ArgumentException("Opcode does not have a field operand", "opCode");
			return new Instruction(opCode, field);
		}

		/// <summary>
		/// Creates a new instruction with a method operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="method">The method</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, IMethod method) {
			if (opCode.OperandType != OperandType.InlineMethod)
				throw new ArgumentException("Opcode does not have a method operand", "opCode");
			return new Instruction(opCode, method);
		}

		/// <summary>
		/// Creates a new instruction with a token operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="token">The token</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, ITokenOperand token) {
			if (opCode.OperandType != OperandType.InlineTok)
				throw new ArgumentException("Opcode does not have a token operand", "opCode");
			return new Instruction(opCode, token);
		}

		/// <summary>
		/// Creates a new instruction with a method signature operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="methodSig">The method signature</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, MethodSig methodSig) {
			if (opCode.OperandType != OperandType.InlineSig)
				throw new ArgumentException("Opcode does not have a method sig operand", "opCode");
			return new Instruction(opCode, methodSig);
		}

		/// <summary>
		/// Creates a new instruction with a method parameter operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="parameter">The method parameter</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, Parameter parameter) {
			if (opCode.OperandType != OperandType.ShortInlineVar && opCode.OperandType != OperandType.InlineVar)
				throw new ArgumentException("Opcode does not have a method parameter operand", "opCode");
			return new Instruction(opCode, parameter);
		}

		/// <summary>
		/// Creates a new instruction with a method local operand
		/// </summary>
		/// <param name="opCode">The opcode</param>
		/// <param name="local">The method local</param>
		/// <returns>A new <see cref="Instruction"/> instance</returns>
		public static Instruction Create(OpCode opCode, Local local) {
			if (opCode.OperandType != OperandType.ShortInlineVar && opCode.OperandType != OperandType.InlineVar)
				throw new ArgumentException("Opcode does not have a method local operand", "opCode");
			return new Instruction(opCode, local);
		}

		/// <summary>
		/// Gets the size in bytes of the instruction
		/// </summary>
		/// <returns></returns>
		public int GetSize() {
			switch (OpCode.OperandType) {
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				return OpCode.Size + 4;

			case OperandType.InlineI8:
			case OperandType.InlineR:
				return OpCode.Size + 8;

			case OperandType.InlineNone:
			case OperandType.InlinePhi:
			default:
				return OpCode.Size;

			case OperandType.InlineSwitch:
				var targets = Operand as IList<Instruction>;
				return OpCode.Size + 4 + (targets == null ? 0 : targets.Count * 4);

			case OperandType.InlineVar:
				return OpCode.Size + 2;

			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
				return OpCode.Size + 1;
			}
		}

		static bool IsSystemVoid(TypeSig type) {
			return type != null && type.ElementType == ElementType.Void;
		}

		/// <summary>
		/// Calculates stack usage
		/// </summary>
		/// <param name="pushes">Updated with number of stack pushes</param>
		/// <param name="pops">Updated with number of stack pops or <c>-1</c> if the stack should
		/// be cleared.</param>
		public void CalculateStackUsage(out int pushes, out int pops) {
			CalculateStackUsage(false, out pushes, out pops);
		}

		/// <summary>
		/// Calculates stack usage
		/// </summary>
		/// <param name="methodHasReturnValue"><c>true</c> if method has a return value</param>
		/// <param name="pushes">Updated with number of stack pushes</param>
		/// <param name="pops">Updated with number of stack pops or <c>-1</c> if the stack should
		/// be cleared.</param>
		public void CalculateStackUsage(bool methodHasReturnValue, out int pushes, out int pops) {
			if (OpCode.FlowControl == FlowControl.Call)
				CalculateStackUsageCall(out pushes, out pops);
			else
				CalculateStackUsageNonCall(methodHasReturnValue, out pushes, out pops);
		}

		void CalculateStackUsageCall(out int pushes, out int pops) {
			pushes = 0;
			pops = 0;

			var method = Operand as IMethod;
			if (method == null)
				return;
			var sig = method.MethodSig;
			if (sig == null)
				return;
			bool implicitThis = sig.HasThis && !sig.ExplicitThis;
			if (!IsSystemVoid(sig.RetType) || (OpCode.Code == Code.Newobj && sig.HasThis))
				pushes++;

			pops += sig.Params.Count;
			if (implicitThis && OpCode.Code != Code.Newobj)
				pops++;
			if (OpCode.Code == Code.Calli)
				pops++;
		}

		void CalculateStackUsageNonCall(bool hasReturnValue, out int pushes, out int pops) {
			StackBehaviour stackBehavior;

			pushes = 0;
			pops = 0;

			stackBehavior = OpCode.StackBehaviourPush;
			switch (stackBehavior) {
			case StackBehaviour.Push0:
				break;

			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
				pushes++;
				break;

			case StackBehaviour.Push1_push1:
				pushes += 2;
				break;

			case StackBehaviour.Varpush:	// only call, calli, callvirt which are handled elsewhere
			default:
				break;
			}

			stackBehavior = OpCode.StackBehaviourPop;
			switch (stackBehavior) {
			case StackBehaviour.Pop0:
				break;

			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				pops++;
				break;

			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				pops += 2;
				break;

			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
			case StackBehaviour.Popref_popi_pop1:
				pops += 3;
				break;

			case StackBehaviour.PopAll:
				pops = -1;
				break;

			case StackBehaviour.Varpop:	// call, calli, callvirt, newobj (all handled elsewhere), and ret
				if (hasReturnValue)
					pops++;
				break;

			default:
				break;
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return InstructionPrinter.ToString(this);
		}
	}
}
