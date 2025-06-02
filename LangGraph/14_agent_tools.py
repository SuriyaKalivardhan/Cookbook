from typing import Annotated, Sequence, TypedDict
from langchain_core.messages import BaseMessage, SystemMessage
from langchain_core.tools import tool
from langchain_openai import ChatOpenAI
from langgraph.graph import StateGraph, START, END
from langgraph.graph.message import add_messages
from langgraph.prebuilt import ToolNode
from dotenv import load_dotenv

load_dotenv()


class AgentState(TypedDict):
    messages: Annotated[Sequence[BaseMessage], add_messages]

@tool
def add(a:int, b:int) -> int:
    '''Adds two numbers'''
    return a+b

@tool
def subtract(a:int, b:int) -> int:
    '''Subtracts two numbers'''
    return a-b

@tool
def multiply(a:int, b:int) -> int:
    '''multiplies two numbers'''
    print("Invoking MULITPLY TOOL")
    return a*b

@tool
def divide(a:int, b:int) -> int:
    '''Divides two numbers'''
    return a//b

tools = [add, subtract, multiply, divide]
tool_node = ToolNode(tools=tools)
model = ChatOpenAI(model='gpt-4o').bind_tools(tools)

def model_call(state:AgentState) -> AgentState:
    sys_msg:SystemMessage = SystemMessage(content="You are an helpful assistant who can assist with the given tools")
    inp = [sys_msg] + state['messages']
    response = model.invoke(inp)
    print(f"AI: {response.content}")
    state['messages'].append(response)
    return state

def shouldContinue(state:AgentState) -> AgentState:
    msg = state['messages'][-1]
    if not msg.tool_calls:
        return "end"
    else:
        return "continue"

graph = StateGraph(AgentState)
graph.add_node("model_node", model_call)
graph.add_node("tool_node", tool_node)
graph.add_edge(START, "model_node")
graph.add_edge('tool_node', "model_node")
graph.add_edge("model_node", END)

graph.add_conditional_edges(
    'model_node',
    shouldContinue,
    {
        'end' : END,
        'continue' : 'tool_node'
    }
)

app = graph.compile()

def print_stream(stream):
    for s in stream:
        message = s["messages"][-1]
        if isinstance(message, tuple):
            print(message)
        else:
            message.pretty_print()


inputs = {"messages": [("user", "Add 3 + 4, then multiply its by 2 and tell me a haiku")]}
print_stream(app.stream(inputs, stream_mode="values"))
    
    